using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossDog : GenericPlayer
{
    private bool selectingRefreshTarget;
    private bool selectedRefreshTarget;
    private Character refreshTarget;

    // Start is called before the first frame update
    void Start()
    {
        PCInit();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(selectingRefreshTarget)
        {
            if(!selectedRefreshTarget)
            {
                Debug.Log("Choosing Target");
                selectedRefreshTarget = AcquireTarget(1, "Player", () => SpecialAttackTargetingFunction());
            }
            else
            {
                Debug.Log("Refresh Target Selected");
                refreshTarget.TurnRefresh();
            }
        }
    }

    protected override void BasicAttack() // Moss Dog's basic attack should heal allies for 2 HP
    {
        Debug.Log(name + " heals " + target.name);
        target.HealDamage(2);
    }

    protected override void SpecialAttack() // Moss Dog's special attack should refresh an ally's actions
    {
        Debug.Log("Executed Moss Dog's special");
        selectingRefreshTarget = true;
        refreshTarget = target;
    }
}