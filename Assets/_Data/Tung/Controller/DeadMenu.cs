using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadMenu : LinkMonoBehaviour
{
    public void Home()
    {
        SceneController.Instance.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneController.Instance.LoadScene(SceneController.Instance.GetActiveSceneName());
        Time.timeScale = 1;
    }
}
