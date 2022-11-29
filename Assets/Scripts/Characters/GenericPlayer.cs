using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GenericPlayer : Character
{
    public GameObject menu;
    protected HUDManager hud;
    public bool isFocusCharacter = true; //when we are swapping between player characters 
    protected string specialTargetTag = "Enemy";
    protected string basicTargetTag = "Enemy";

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
    protected override void Update()
    {
        if(turn && isFocusCharacter) {
            if(hud.moveButtonActive && !moved) {
                Debug.Log(name + " move mode");
                move.BeginTurn();
                moved = true;
            }
            else if(hud.basicAttackActive && !usedBasic) {
               // Debug.Log(name + " basic attack mode");
                if(!selectedBasicTarget) {
                    selectedBasicTarget = AcquireTarget(basicAttackRange, basicTargetTag);
                }
                else{
                    BasicAttack();
                    usedBasic = true;
                }

            }
            else if(hud.specialAttackActive && currSpecialCooldown == 0) {
                Debug.Log(name + " special attack mode");
                if(!selectedSpecialTarget) {
                    selectedSpecialTarget = AcquireTarget(specialAttackRange, specialTargetTag);
                }
                else{
                    SpecialAttack();
                    currSpecialCooldown = specialAttackCooldown;
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
            Debug.Log("reset booleans");

            if(currSpecialCooldown < 0) {
                currSpecialCooldown = 0;
            }

        }
    }

    //the boolean return is to tell the attack functions whether the attack has successfully obtained a valid target or not

    public bool AcquireTarget(int range, string desiredTarget){
        Vector3 halfExtents = new Vector3(range, 4, range);
        Collider[] colliders = Physics.OverlapBox(transform.position, halfExtents);

        foreach (Collider item in colliders)
        {
            Tile tile = item.GetComponent<Tile>();
            if (tile != null)
            {
                Debug.Log("attackable set");
                tile.attackable = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                
                if (hit.collider.tag == desiredTarget &&
                colliders.Contains(hit.collider))
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

        return false;
    }
}
