using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    GameObject HUD;
    [SerializeField]
    Button moveButton, basicButton, specialButton, secondSpecialButton, endTurnButton;
    public bool moveButtonActive = false;
    public bool basicAttackActive = false;
    public bool specialAttackActive = false;
    public bool secondSpecialAttackActive = false;

    void Start() {
        moveButton.onClick.AddListener(MoveButton);
        basicButton.onClick.AddListener(BasicButton);
        specialButton.onClick.AddListener(SpecialButton);
        secondSpecialButton.onClick.AddListener(SecondSpecialButton);
        endTurnButton.onClick.AddListener(EndButton);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) {
            if (HUD.activeInHierarchy) {
                HUD.SetActive(false);
            }
            else {
                HUD.SetActive(true);
            }
        }
    }

    private void MoveButton () {
        if (moveButtonActive) {
            moveButtonActive = false;
        }
        else {
            moveButtonActive = true;
        }
        Debug.Log("MOVE button pressed, is now " + moveButtonActive);
    }

    private void BasicButton () {
        if (basicAttackActive) {
            basicAttackActive = false;
        }
        else {
            basicAttackActive = true;
        }
        Debug.Log("BASIC ATTACK button pressed, is now " + basicAttackActive);
    }

    private void SpecialButton () {
        if (specialAttackActive) {
            specialAttackActive = false;
        }
        else {
            specialAttackActive = true;
        }
        Debug.Log("SPECIAL ATTACK button pressed, is now " + specialAttackActive);
    }

    private void SecondSpecialButton () {
        if (secondSpecialAttackActive) {
            secondSpecialAttackActive = false;
        }
        else {
            secondSpecialAttackActive = true;
        }
        Debug.Log("SECOND SPECIAL ATTACK button pressed, is now " + secondSpecialAttackActive);
    }

    private void EndButton () {
        Debug.Log("END TURN button pressed");
    }

}
