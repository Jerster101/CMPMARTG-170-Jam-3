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
    [SerializeField] private HUDManager HUD;
    [SerializeField] protected int magic = 5;
    [SerializeField] protected int strength = 5;
    [SerializeField] protected int health = 5;
    [SerializeField] protected int maxHealth = 5; // Variable used for Moss Dog's healing basic attack. This is mainly used to check for if the max health of an ally would be reached.
    [SerializeField] protected int healing = 2; // Variable used for Moss Dog's healing basic attack
    [SerializeField] protected float baseAccuracy = 0.8f; //Default chance to hit with attacks    
    [SerializeField] protected int basicAttackRange = 1;
    [SerializeField] protected int specialAttackRange = 2;
    [SerializeField] protected CameraShake basicAttackShake;
    [SerializeField] protected CameraShake specialAttackShake;
    [SerializeField] protected AudioSource basicAttackSound;
    [SerializeField] protected AudioSource specialAttackSound;
    [SerializeField] protected AudioSource walkSound;
    [SerializeField] public int turnOrder = -1;
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
        if (move.moving)
        {
            if (!walkSound.isPlaying)
                walkSound.Play();
        }

        /*
        if (turn) {
            if(isDead) {
                Debug.Log(name + " is dead, ending turn");
                EndTurn();
            }
        }
        */
        
    }

    virtual protected void BasicAttack() {
        Debug.Log(name + " uses Basic Attack targeting " + target.name);
        if(Random.value <= baseAccuracy) {
            target.TakeDamage(attackStat);
        }
    }

    virtual protected void SpecialAttack() {
        Debug.Log(name + " uses Basic Attack targeting " + target.name);
        if(Random.value <= baseAccuracy) {
            target.TakeDamage(attackStat);
        }
    }

    virtual public void RefreshActions()
    {
        // can be inherited to refresh self actions, does nothing by default
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if(health <= 0) {isDead = true;}

        switch (name)
        {
            case "Frog":
                HUD.UpdateHP(0, health, true);
                break;
            case "Witch":
                HUD.UpdateHP(0, health, true);
                break;
            case "Moss Dog":
                HUD.UpdateHP(0, health, true);
                break;
            default:
                Debug.LogError("bad name in Character.cs");
                break;
        }
    }

    public void HealDamage(int healing)
    {
        if (health + healing <= maxHealth) // if the new health value after healing is at most the max health, heal the amount
        {
            health += healing;
        }
        else // else make the new health value equal to the max health
        {
            health = maxHealth;
        }
    }

    public virtual void BeginTurn()
    {
        if(!turn) {
            turn = true; 
            move.BeginTurn();
        }

        switch (name)
        {
            case "Frog":
                HUD.MoveTurnMarker(1);
                break;
            case "Witch":
                HUD.MoveTurnMarker(2);
                break;
            case "Moss Dog":
                HUD.MoveTurnMarker(3);
                break;
            default:
                Debug.LogError("bad name in Character.cs");
                break;
        }

        Debug.Log(name + " begins their turn");
        
    }

    public virtual void EndTurn()
    {
        if(turn) {
            turn = false;  
        }

        if (name == "Moss Dog")
        {
            HUD.MoveTurnMarker(0);
        }

        Debug.Log(name + " ends their turn");
        
    }

}
