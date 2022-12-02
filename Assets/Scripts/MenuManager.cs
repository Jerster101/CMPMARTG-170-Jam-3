using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

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
