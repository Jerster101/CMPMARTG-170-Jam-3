using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : GenericPlayer
{
    private List<Character> aoeTargets = new List<Character>();
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
    override protected void SpecialAttack(){
        foreach(Character c in aoeTargets) {
            Debug.Log("witch aoe hits " + c.name);
            c.TakeDamage(magic);
        }
        aoeTargets.Clear();

    }
    override protected void BasicAttack() {
        foreach(Character c in aoeTargets) {
            Debug.Log("witch aoe hits " + c.name);
            c.TakeDamage(magic);
        }
        aoeTargets.Clear();

    }

    override public bool AcquireTarget(int range, string desiredTarget, TargetingFunction targetingFunction) {
        Collider[] colliders = targetingFunction();

        foreach(Collider item in colliders) {
            if(item.tag == desiredTarget) {
                aoeTargets.Add(item.GetComponent<Character>());
            }
            Tile tile = item.GetComponent<Tile>();
            if (tile != null)
            {
                tile.attackable = true;
            }

        }

        if(aoeTargets.Count > 0) {
            return true;
        }
        else{
            Debug.Log("no targets in aoe");
            return false;
        }
    }


}
