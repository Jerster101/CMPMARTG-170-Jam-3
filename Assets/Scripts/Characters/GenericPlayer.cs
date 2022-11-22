using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPlayer : Character
{
    protected void PCInit() {
        Init();

        gameObject.tag = "Player";
    }

    void Start() {
        PCInit();
    }

    // Update is called once per frame
    void Update()
    {
        if(turn) {
            AcquireTarget();
        }
        
    }

    public override void BeginTurn()
    {
        if(!turn) {
            turn = true;
        }
    }

    public Character AcquireTarget(){
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.tag + " hit by AcquireTarget raycast");
                if (hit.collider.tag == "Enemy")
                {
                    target = hit.collider.GetComponent<Character>();
                    Debug.Log(name + " switches targets to " + target.name);
                    move.BeginTurn();
                }
            }
        }
        return target;

    }
}
