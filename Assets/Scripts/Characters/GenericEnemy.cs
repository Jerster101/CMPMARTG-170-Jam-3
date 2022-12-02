using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class GenericEnemy : Character
{
    protected bool moved = false;
    protected bool triedTargeting = false;
    protected bool waiting = false;
    // Start is called before the first frame update
    virtual protected void Start()
    {
        Init();

        gameObject.tag = "Enemy";
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(turn) {
            // if haven't moved, then move
            if(!moved) {
                // check if not currently moving
                if (!move.moving)
                {
                    if (!waiting)
                    {
                        move.BeginTurn();
                        waiting = true;
                    }
                    else
                    {
                        moved = true;
                        waiting = false;
                    }
                }
            }
        }
        
        if(moved) {
            // check for attack
            if (!triedTargeting)
            {
                if (!waiting)
                {
                    AcquireTarget(1, "Player", () => BasicAttackTargetingFunction());
                    triedTargeting = true;
                }
            }
            else
            {
                // target selection is done
                if (!waiting)
                {
                    // valid target found
                    if (target)
                    {
                        // Debug.Log("Target Found");
                        if (basicAttackShake != null)
                            basicAttackShake.StartShake();
                        BasicAttack();
                    }
                    
                    TurnManager.EndTurn();
                }
            }
        }
    }

    virtual protected Collider[] BasicAttackTargetingFunction()
    {
        Vector3 halfExtents = new Vector3(0.5f, 4, 0.5f);
        return Physics.OverlapBox(transform.position, halfExtents, Quaternion.Euler(0, 45, 0));
    }

    protected IEnumerator WaitAndCleanUpTargeting(Collider[] colliders, float waitTime)
    {
        waiting = true;
        yield return new WaitForSeconds(waitTime);

        // reset tiles
        foreach (Collider item in colliders)
        {
            Tile tile = item.GetComponent<Tile>();
            if (tile != null)
            {
                tile.Reset();
            }
        }

        waiting = false;
    }
    public delegate Collider[] TargetingFunction();
    public void AcquireTarget(int range, string desiredTarget, TargetingFunction targetingFunction)
    {
        Collider[] colliders = targetingFunction();

        // color tiles
        foreach (Collider item in colliders)
        {
            Tile tile = item.GetComponent<Tile>();
            if (tile != null)
            {
                tile.attackable = true;
            }
        }

        // select target
        foreach (Collider item in colliders)
        {
            Character character = item.GetComponent<Character>();
            if (character != null)
            {
                if (character.CompareTag(desiredTarget))
                {
                    target = character;
                    // color as target
                    Tile tile = move.GetTargetTile(target.gameObject);
                    tile.target = true;
                }
            }
        }

        // launch wait and clean up tiles coroutine
        StartCoroutine(WaitAndCleanUpTargeting(colliders, 0.6f));
    }

    public override void BeginTurn(){
        moved = false;
        target = null;
        if (!turn) {turn = true;}
    }

    public override void EndTurn()
    {
        moved = false;
        waiting = false;
        triedTargeting = false;

        base.EndTurn();
    }
}
