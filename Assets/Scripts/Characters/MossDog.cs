using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossDog : GenericPlayer
{
    // Start is called before the first frame update
    void Start()
    {
        PCInit();
    }
    // Update is called once per frame
    void Update()
    {
        public override void BasicAttack()
        {
            Debug.Log(name + " heals " + target.name);
            target.HealDamage();
        }
    }
}
