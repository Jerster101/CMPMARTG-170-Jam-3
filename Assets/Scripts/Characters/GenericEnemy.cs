using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemy : Character
{
    // Start is called before the first frame update
    void Start()
    {
        Init();

        gameObject.tag = "Enemy";
        targetableTag = "Player";
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
