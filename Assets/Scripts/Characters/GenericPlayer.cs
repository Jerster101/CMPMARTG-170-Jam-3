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
        base.Update();

        if(turn) { //  && isFocusCharacter
            hud.EnableButtons();
            // stop move action when it is cancelled
            if (!hud.moveButtonActive && move.turn && !move.moving)
            {
                move.EndTurn();
                moved = false;
                Tile.ResetAllTiles();
            }
                
            if(hud.moveButtonActive && !moved) {
                Debug.Log(name + " move mode");
                move.BeginTurn();
                moved = true;
            }
            else if(hud.basicAttackActive && !usedBasic) {
               // Debug.Log(name + " basic attack mode");
                if(!selectedBasicTarget) {
                    selectedBasicTarget = AcquireTarget(basicAttackRange, basicTargetTag, () => BasicAttackTargetingFunction());
                }
                else{
                    if (basicAttackSound != null)
                        basicAttackSound.Play();
                    if (basicAttackShake != null)
                        basicAttackShake.StartShake();
                    Tile.ResetAllTiles();
                    BasicAttack();
                    usedBasic = true;
                }

            }
            else if(hud.specialAttackActive && currSpecialCooldown == 0) {
                Debug.Log(name + " special attack mode");
                if(!selectedSpecialTarget) {
                    selectedSpecialTarget = AcquireTarget(specialAttackRange, specialTargetTag, () => SpecialAttackTargetingFunction());
                }
                else{
                    if (specialAttackSound != null)
                        specialAttackSound.Play();
                    if (specialAttackShake != null)
                        specialAttackShake.StartShake();
                    Tile.ResetAllTiles();
                    SpecialAttack();
                    currSpecialCooldown = specialAttackCooldown;
                }
            }
        }
        // if (!turn) hud.DisableButtons();
        
    }

    public override void RefreshActions()
    {
        selectedSpecialTarget = false;
        selectedBasicTarget = false;
        moved = false;
        usedBasic = false;
        Debug.Log("refreshed actions");
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

    virtual protected Collider[] BasicAttackTargetingFunction()
    {
        Vector3 halfExtents = new Vector3(basicAttackRange, 4, basicAttackRange);
        return Physics.OverlapBox(transform.position, halfExtents, Quaternion.Euler(0, 45, 0));
    }

    virtual protected Collider[] SpecialAttackTargetingFunction()
    {
        Vector3 halfExtents = new Vector3(specialAttackRange, 4, specialAttackRange);
        return Physics.OverlapBox(transform.position, halfExtents, Quaternion.Euler(0, 45, 0));
        Debug.Log("Targeting og");
    }

    public delegate Collider[] TargetingFunction();
    //the boolean return is to tell the attack functions whether the attack has successfully obtained a valid target or not
    
    virtual public bool AcquireTarget(int range, string desiredTarget, TargetingFunction targetingFunction)
    {
        Collider[] colliders = targetingFunction();

        foreach (Collider item in colliders)
        {
            Tile tile = item.GetComponent<Tile>();
            if (tile != null)
            {
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

                    if(desiredTarget == "Player" || desiredTarget == "Enemy") {
                        target = hit.collider.GetComponent<Character>();
                    }
                    else if (desiredTarget == "Tile") {
                        Debug.Log("Found a tile");
                        tileTarget = hit.collider.GetComponent<Tile>();
                    }
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
