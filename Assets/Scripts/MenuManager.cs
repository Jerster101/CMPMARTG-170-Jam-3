using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject pauseMenu;

    public void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            if (pauseMenu.activeInHierarchy) {
                pauseMenu.SetActive(false);
            }
            else {
                pauseMenu.SetActive(true);
            }
        }
    }

    public void EndGame() {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void PlayGame() {
        SceneManager.LoadScene("TacticalRPG", LoadSceneMode.Single);
    }

    public void Die() {
        Application.Quit();
    }
}
