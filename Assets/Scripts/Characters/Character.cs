using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Elizabeth's code :) ask me if you have questions
//The idea is that we can use this script for player characters and enemies- we can inherit off of this
public class Character : MonoBehaviour
{
    public string name = "Default";

    //stats - we can change this later
    [SerializeField] int magic = 5;
    [SerializeField] int strength = 5;
    [SerializeField] int speed = 5;
    [SerializeField] int health = 5;
    [SerializeField] float baseAccuracy = 0.8f; //Default chance to hit with attacks

    protected bool isDead = false;
    protected bool isMyTurn = false;
    protected int attackStat; //easy way to change which stat is used for damage calculations

    public Character target; //use this to deal damage to other characters 

    


    // Start is called before the first frame update
    void Start()
    {
        attackStat = strength; //setting which stat to use for attacking
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead) {

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

}
