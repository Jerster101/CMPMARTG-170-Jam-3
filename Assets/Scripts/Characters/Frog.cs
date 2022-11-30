using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : GenericPlayer
{
    private bool selectingGrabTile;
    private bool selectedGrabTile;
    private Character grabTarget;
    // Start is called before the first frame update
    void Start()
    {
        PCInit();
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if(selectingGrabTile) {
            if(!selectedGrabTile) {
                Debug.Log("Selecting Grab Tile");
                selectedGrabTile = AcquireTarget(specialAttackRange, "Tile", () => SpecialAttackTargetingFunction());
            }
            else{
                Debug.Log("Moving to tile");
                grabTarget.move.MoveToTile(tileTarget);
            }
        }
    }

    override protected Collider[] SpecialAttackTargetingFunction() {
        Vector3 halfExtents = new Vector3(specialAttackRange, 4, specialAttackRange);
        return Physics.OverlapBox(transform.position, halfExtents, Quaternion.Euler(0, 45, 0));
        Debug.Log("using frog targeting");

    }

    override protected void SpecialAttack() {
        Debug.Log("Executed frog specialattack");
        selectingGrabTile = true;
        grabTarget = target;

    }
}
