using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossDog : GenericPlayer
{
    // Start is called before the first frame update
    void Start()
    {
        PCInit();
        // override special attack target
        basicTargetTag = "Player";
        specialTargetTag = "Player";
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void BasicAttack() // Moss Dog's basic attack should heal allies for 2 HP
    {
        Debug.Log(name + " heals " + target.name);
        target.HealDamage(2);
    }

    protected override void SpecialAttack() // Moss Dog's special attack should refresh an ally's actions
    {
        Debug.Log("Executed Moss Dog's special");
        target.RefreshActions();
    }
}