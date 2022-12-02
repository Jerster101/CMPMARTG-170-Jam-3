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
    

    [SerializeField]
    GameObject HUD;
    [SerializeField]
    Button moveButton, basicButton, specialButton, secondSpecialButton, endTurnButton;
    public bool moveButtonActive = false;
    public bool basicAttackActive = false;
    public bool specialAttackActive = false;
    public bool secondSpecialAttackActive = false;

    bool menuOpen = true;

    void Start() {
        moveButton.onClick.AddListener(MoveButton);
        basicButton.onClick.AddListener(BasicButton);
        specialButton.onClick.AddListener(SpecialButton);
        secondSpecialButton.onClick.AddListener(SecondSpecialButton);
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
        HideMenu();
    }

    private void BasicButton () {
        basicAttackActive = true;
        if (SHOWDEBUGINFO) Debug.Log("BASIC ATTACK button pressed, is now " + basicAttackActive);
        HideMenu();
    }

    private void SpecialButton () {
        specialAttackActive = true;
        if (SHOWDEBUGINFO) Debug.Log("SPECIAL ATTACK button pressed, is now " + specialAttackActive);
        HideMenu();
    }

    private void SecondSpecialButton () {
        secondSpecialAttackActive = true;
        if (SHOWDEBUGINFO) Debug.Log("SECOND SPECIAL ATTACK button pressed, is now " + secondSpecialAttackActive);
        HideMenu();
    }

    private void EndButton () {
        if (SHOWDEBUGINFO) Debug.Log("END TURN button pressed");
        moveButtonActive = false;
        basicAttackActive = false;
        specialAttackActive = false;
        secondSpecialAttackActive = false;
        TurnManager.EndTurn();
    }

    public void DisableButtons()
    {
        moveButton.interactable = false;
        basicButton.interactable = false;
        specialButton.interactable = false;
        secondSpecialButton.interactable = false;
        endTurnButton.interactable = false;
    }

    public void EnableButtons()
    {
        moveButton.interactable = true;
        basicButton.interactable = true;
        specialButton.interactable = true;
        secondSpecialButton.interactable = true;
        endTurnButton.interactable = true;
    }

    private void HideMenu() {
        menuOpen = false;
        HUD.SetActive(false);
        if (SHOWDEBUGINFO) Debug.Log("Button pushed, HUD hidden");
    }

    private void CancelAction() {
        moveButtonActive = false;
        basicAttackActive = false;
        specialAttackActive = false;
        secondSpecialAttackActive = false;
        if (SHOWDEBUGINFO) Debug.Log("Action canceled, all values set to false");
        ShowMenu();
    }

    public void ShowMenu() {
        menuOpen = true;
        HUD.SetActive(true);
        if (SHOWDEBUGINFO) Debug.Log("HUD shown");
    }

}
