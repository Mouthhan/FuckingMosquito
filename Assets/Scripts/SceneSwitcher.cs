using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void SwitchToMainGame()
    {
        Debug.Log("To maingame");
        SceneManager.LoadScene("Scenes/MainGame");
    }

    public void SwitchToMenu()
    {
        Debug.Log("To menu");
        SceneManager.LoadScene("Scenes/Menu");
    }
}
