using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemy : Character
{
    protected bool moved = false;
    // Start is called before the first frame update
    void Start()
    {
        Init();

        gameObject.tag = "Enemy";
    }

    // Update is called once per frame
    void Update()
    {
        if(turn) {
            if(!moved) {
                moved = true;
                move.BeginTurn();
            }
        }
        //add any other bools for if it took other actions here so it will end its turn if all the bools are true
        if(moved) {
            Debug.Log("Problem?");
            TurnManager.EndTurn();
        }
    }

    public override void BeginTurn(){
        moved = false;
        if (!turn) {turn = true;}
    }

    public override void EndTurn()
    {
        if (moved)
            moved = false;

        base.EndTurn();
    }
}
