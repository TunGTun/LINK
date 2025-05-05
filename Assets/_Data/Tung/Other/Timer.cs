using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : LinkMonoBehaviour
{
    public TextMeshProUGUI timerText;
    float elapsedTime;

    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private CharCtrl charCtrl;

    void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (Input.GetKeyDown(KeyCode.P) && !charCtrl.CharState.GetIsDead())
        {
            this.pauseMenu.Pause();
        }
    }
}
