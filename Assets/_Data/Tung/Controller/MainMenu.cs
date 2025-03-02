using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : LinkMonoBehaviour
{
    //[Header("MainMenu")]
    public void PlayGame()
    {
        SceneController.Instance.PlayGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
