using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    #pragma warning disable 0162
    //turn UI debug info on/off
    const bool SHOWDEBUGINFO = false;

    [SerializeField] TMP_Text fHealth;
    [SerializeField] TMP_Text wHealth;
    [SerializeField] TMP_Text mHealth;
    [SerializeField] Image fPortrait;
    [SerializeField] Image wPortrait;
    [SerializeField] Image mPortrait;

    [SerializeField] Sprite fNeutral;
    [SerializeField] Sprite fDamage;
    [SerializeField] Sprite fHappy;
    [SerializeField] Sprite wNeutral;
    [SerializeField] Sprite wDamage;
    [SerializeField] Sprite wHappy;
    [SerializeField] Sprite mNeutral;
    [SerializeField] Sprite mDamage;
    [SerializeField] Sprite mHappy;

    [SerializeField] GameObject turnMarker;
    [SerializeField] GameObject HUD;
    [SerializeField] Button moveButton, basicButton, specialButton, endTurnButton;
    //[SerializeField]
    //AudioSource onClickSound;
    public bool moveButtonActive = false;
    public bool basicAttackActive = false;
    public bool specialAttackActive = false;

    bool menuOpen = true;

    void Start() {
        moveButton.onClick.AddListener(MoveButton);
        basicButton.onClick.AddListener(BasicButton);
        specialButton.onClick.AddListener(SpecialButton);
        endTurnButton.onClick.AddListener(EndButton);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1) && !menuOpen)
        {
            CancelAction();
        }
    }

    private void MoveButton () {
        moveButtonActive = true;
        if (SHOWDEBUGINFO) Debug.Log("MOVE button pressed, is now " + moveButtonActive);
        //onClickSound.Play();
        HideMenu();
    }

    private void BasicButton () {
        basicAttackActive = true;
        if (SHOWDEBUGINFO) Debug.Log("BASIC ATTACK button pressed, is now " + basicAttackActive);
        //onClickSound.Play();
        HideMenu();
    }

    private void SpecialButton () {
        specialAttackActive = true;
        if (SHOWDEBUGINFO) Debug.Log("SPECIAL ATTACK button pressed, is now " + specialAttackActive);
        //onClickSound.Play();
        HideMenu();
    }

    private void EndButton () {
        if (SHOWDEBUGINFO) Debug.Log("END TURN button pressed");
        //onClickSound.Play();
        moveButtonActive = false;
        basicAttackActive = false;
        specialAttackActive = false;
        TurnManager.EndTurn();
    }

    public void DisableButtons()
    {
        moveButton.interactable = false;
        basicButton.interactable = false;
        specialButton.interactable = false;
        endTurnButton.interactable = false;
    }

    public void EnableButtons()
    {
        moveButton.interactable = true;
        basicButton.interactable = true;
        specialButton.interactable = true;
        endTurnButton.interactable = true;
    }

    private void HideMenu() {
        menuOpen = false;
        HUD.SetActive(false);
        if (SHOWDEBUGINFO) Debug.Log("Button pushed, HUD hidden");
    }

    private void CancelAction() {
        Tile.ResetAllTiles(); // technically shouldn't happen when it is a move action but oh well
        moveButtonActive = false;
        basicAttackActive = false;
        specialAttackActive = false;
        if (SHOWDEBUGINFO) Debug.Log("Action canceled, all values set to false");
        ShowMenu();
    }

    public void ShowMenu() {
        menuOpen = true;
        HUD.SetActive(true);
        if (SHOWDEBUGINFO) Debug.Log("HUD shown");
    }

    public void MoveTurnMarker(int x) {
        switch (x)
        {
            case 0:
                turnMarker.transform.localPosition = new Vector3(-1039, 722, 0);
                break;
            case 1:
                turnMarker.transform.localPosition = new Vector3(-864, 722, 0);
                break;
            case 2:
                turnMarker.transform.localPosition = new Vector3(-684, 722, 0);
                break;
            case 3:
                turnMarker.transform.localPosition = new Vector3(-498, 722, 0);
                break;
            default:
                Debug.LogError("Out of bounds error in function MoveTurnMarker().  Int should be no greater than 3, and no lower than 0");
                break;
        }
    }

    public void UpdateHP(int character, int value, bool animate)
    {
        string text = "HP: " + value + "/5";
        switch (character)
        {
            case 0:
                fHealth.text = text;
                FrogAnimate();
                break;
            case 1:
                wHealth.text = text;
                break;
            case 2:
                mHealth.text = text;
                break;
            default:
                Debug.LogError("Out of bounds error in function UpdateHP().  Int should be no greater than 2, and no lower than 0");
                break;
        }
    }

    private void FrogAnimate()
    {
        fPortrait.sprite = fDamage;
        StartCoroutine(Wait(1f));
    }

    IEnumerator Wait(float time)
    {

        yield return new WaitForSeconds(time);
        fPortrait.sprite = fNeutral;
    }

}
