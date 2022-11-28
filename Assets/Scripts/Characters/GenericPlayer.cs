using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPlayer : Character
{
    public GameObject menu;
    protected HUDManager hud;
    public bool isActiveCharacter = true; //when we are swapping between player characters 
    protected string specialTargetTag = "Enemy";

    [SerializeField] int specialAttackCooldown = 2;
    private int currSpecialCooldown = 0;

    private bool selectedSpecialTarget = false;
    private bool selectedBasicTarget = false;
    private bool usedBasic = false;
    private bool moved = false;

    protected void PCInit() {
        Init();

        gameObject.tag = "Player";
        hud = menu.GetComponent<HUDManager>();
    }

    void Start() {
        PCInit();
    }

    // Update is called once per frame
    void Update()
    {
        if(turn && isFocusCharacter) {
            if(hud.moveButtonActive && !moved) {
                move.BeginTurn();
                moved = true;
            }
            else if(hud.basicAttackActive && !usedBasic) {
                if(!selectedBasicTarget) {
                    selectedBasicTarget = AcquireTarget(basicAttackRange, "Enemy");
                }
                else{
                    BasicAttack(target);
                    usedBasic = true;
                }

            }
            else if(specialAttackActive && currSpecialCooldown == 0) {
                if(!selectedSpecialTarget) {
                    selectedSpecialTarget = AcquireTarget(specialAttackRange, specialTargetTag);
                }
                else{
                    SpecialAttack(target);
                    currSpecialCooldown = SpecialAttackCooldown;
                }
            }
        }
        
    }

    public override void BeginTurn()
    {
        if(!turn) {
            turn = true;
            selectedSpecialTarget = false;
            selectedBasicTarget = false;
            moved = false;
            usedBasic = false;
            currSpecialCooldown -= 1;

            if(currSpecialCooldown < 0) {
                currSpecialCooldown = 0;
            }

        }
    }

    //the boolean return is to tell the attack functions whether the attack has successfully obtained a valid target or not

    public bool AcquireTarget(int range, string desiredTarget){
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == desiredTarget)
                {
                    target = hit.collider.GetComponent<Character>();
                    Debug.Log(name + " switches targets to " + target.name);
                    return true;
                }
            }
            else {
                Debug.Log("AcquireTarget failed to find target this frame");
                return false;
            }
        }

    }
}
