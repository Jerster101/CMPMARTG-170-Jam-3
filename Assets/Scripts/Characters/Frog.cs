using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : GenericPlayer
{
    // Start is called before the first frame update
    void Start()
    {
        PCInit();
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    override protected Collider[] SpecialAttackTargetingFunction() {
        Vector3 halfExtents = new Vector3(specialAttackRange, 4, specialAttackRange);
        return Physics.OverlapBox(transform.position, halfExtents, Quaternion.Euler(0, 45, 0));
        Debug.Log("using frog targeting");

    }

    override protected void SpecialAttack() {
        Vector3 frogPos = transform.position;
        Vector3 upTile = new Vector3 (frogPos.x, frogPos.y, frogPos.z + 1f);
        Vector3 downTile = new Vector3 (frogPos.x, frogPos.y, frogPos.z - 1f);
        Vector3 leftTile = new Vector3 (frogPos.x + 1f, frogPos.y, frogPos.z);
        Vector3 rightTile = new Vector3 (frogPos.x - 1f, frogPos.y, frogPos.z);
        Vector3[] adjTiles = new Vector3[] {upTile, leftTile, downTile, rightTile};

        Vector3 lowest = upTile;

        foreach(Vector3 tile in adjTiles) {
            if(Vector3.Distance(transform.position, tile) <= Vector3.Distance(transform.position, lowest)) {
                lowest = tile;
            }
        }
        target.transform.position = lowest;
    }
}
