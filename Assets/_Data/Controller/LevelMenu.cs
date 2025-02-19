using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            levels[i].interactable = true;
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
        SceneManager.LoadScene(levelName);
    }
}
