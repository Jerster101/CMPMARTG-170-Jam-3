using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Elizabeth's code :) ask me if you have questions
//The idea is that we can use this script for player characters and enemies- we can inherit off of this
public class Character : MonoBehaviour
{
    //Allows character and move scripts to talk to each other when attached to the same obj
    private TacticsMove move;
    public string name = "Default";

    //stats - we can change this later
    [SerializeField] int magic = 5;
    [SerializeField] int strength = 5;

    [SerializeField] int health = 5;
    [SerializeField] float baseAccuracy = 0.8f; //Default chance to hit with attacks
    private bool isDead = false;
    private bool turn = false;
    protected int attackStat; //easy way to change which stat is used for damage calculations

    public Character target; //use this to deal damage to other characters 

    protected string targetableTag = "Character";

    


    //use for initialization
    protected void Init()
    {
        attackStat = strength; //setting which stat to use for attacking
        move = GetComponent<TacticsMove>();

        TurnManager.AddUnit(this);

        gameObject.tag = "Character";
        
    }

    // Update is called once per frame
    void Update()
    {
        if(turn) {
            if(isDead) {
                Debug.Log(name + " is dead, ending turn");
                EndTurn();
            }

        }
        
    }

    virtual protected void BasicAttack() {
        Debug.Log(name + " uses Basic Attack targeting " + target.name);
        if(Random.value <= baseAccuracy) {
            target.TakeDamage(attackStat);
        }
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if(health <= 0) {isDead = true;}
    }

    //sets the current target to whatever character is clicked on
    public void AcquireTarget() {
        if(Input.GetMouseButtonUp(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)) {
                if (hit.collider.tag == targetableTag) {
                    target = hit.collider.GetComponent<Character>();
                    Debug.Log( name + " switches targets to " + target.name);
                }
            }
        }

    }

    public void BeginTurn()
    {
        //Elizabeth- since TacticsMove.BeginTurn() and Character.BeginTurn() can call each other, trying to prevent loops
        //Is it inefficient? probably. but we can make it better later
        if(!turn) {
            turn = true;
            move.BeginTurn(); 
        }

        Debug.Log(name + "begins their turn");
        
    }

    public void EndTurn()
    {
        if(turn) {
            turn = false;
            move.EndTurn();  
        }

        Debug.Log(name + " ends their turn");
        
    }

}
