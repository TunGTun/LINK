using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : LinkMonoBehaviour
{
    private static SceneController _instance;
    public static SceneController Instance { get => _instance; }

    protected override void Awake()
    {
        base.Awake();
        if (SceneController._instance != null) Debug.LogError("Only 1 SceneController allow to exist");
        SceneController._instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void NextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
