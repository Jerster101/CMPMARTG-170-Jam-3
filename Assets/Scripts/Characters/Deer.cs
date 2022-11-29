using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : GenericEnemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override Collider[] BasicAttackTargetingFunction()
    {
        // larger targeting range
        Vector3 halfExtents = new Vector3(1.2f, 4, 1.2f);
        return Physics.OverlapBox(transform.position, halfExtents, Quaternion.Euler(0, 45, 0));
    }
}
