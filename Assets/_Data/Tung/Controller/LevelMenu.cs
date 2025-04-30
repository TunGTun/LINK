using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : LinkMonoBehaviour
{
    [SerializeField] protected Button[] levels;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadButtonLevels();
        this.LoadLevel();
    }

    protected virtual void LoadLevel()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            levels[levels.Length - 1 - i].interactable = true;
        }
    }

    protected virtual void LoadButtonLevels()
    {
        if (levels.Length > 0) return;
        this.levels = GetComponentsInChildren<Button>();
        Debug.LogWarning(transform.name + ": LoadButtonLevels", gameObject);
    }

    public void OpenLevel(int levelId)
    {
        string levelName = "Level" + levelId;
        SceneController.Instance.LoadScene(levelName);
    }

    public void Cancel()
    {
        SceneController.Instance.LoadScene("MainMenu");
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("ReachedIndex", 1);
        PlayerPrefs.SetInt("UnlockedLevel", 1);
        PlayerPrefs.Save();
        SceneController.Instance.LoadScene("SelectLevel");
    }
}
