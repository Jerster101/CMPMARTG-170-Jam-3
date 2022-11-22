using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : Character
{
    // Start is called before the first frame update
    void Start()
    {
        Init();

        gameObject.tag = "Player";
        targetableTag = "Enemy";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
