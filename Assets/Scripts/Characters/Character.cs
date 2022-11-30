using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Elizabeth's code :) ask me if you have questions
//The idea is that we can use this script for player characters and enemies- we can inherit off of this
public class Character : MonoBehaviour
{
    //Allows character and move scripts to talk to each other when attached to the same obj
    public TacticsMove move;
    public string name = "Default";

    //stats - we can change this later
    [SerializeField] int magic = 5;
    [SerializeField] int strength = 5;
    [SerializeField] int health = 5;
    [SerializeField] int healing = 2; // Variable used for Moss Dog's healing basic attack
    [SerializeField] float baseAccuracy = 0.8f; //Default chance to hit with attacks    
    [SerializeField] protected int basicAttackRange = 1;
    [SerializeField] protected int specialAttackRange = 2;
    protected bool isDead = false;
    protected bool turn = false;
    protected int attackStat; //easy way to change which stat is used for damage calculations

    public Character target; //use this to store other characters to target with attacks
    public Tile  tileTarget;

    //use for initialization
    protected void Init()
    {
        attackStat = strength; //setting which stat to use for attacking
        move = GetComponent<TacticsMove>();

        TurnManager.AddUnit(this);

        Debug.Log("Added player to turnmanager");

        gameObject.tag = "Character";

        tileTarget = move.currentTile;
        
    }

    // Update is called once per frame
    protected virtual void Update()
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

    virtual protected void SpecialAttack() {
        Debug.Log(name + " uses Special Attack targeting " + target.name);
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if(health <= 0) {isDead = true;}
    }

    public void HealDamage(int healing)
    {
        health += healing;
    }

    public virtual void BeginTurn()
    {
        if(!turn) {
            turn = true; 
            move.BeginTurn();
        }

        Debug.Log(name + " begins their turn");
        
    }

    public virtual void EndTurn()
    {
        if(turn) {
            turn = false;  
        }

        Debug.Log(name + " ends their turn");
        
    }

}
