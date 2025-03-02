using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : LinkMonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Home()
    {
        SceneController.Instance.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;

    }

    public void Restart()
    {
        SceneController.Instance.LoadScene(SceneController.Instance.GetActiveSceneIndex());
        Time.timeScale = 1;
    }
}
