using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : LinkMonoBehaviour
{
    public TextMeshProUGUI timerText;
    float elapsedTime;

    [SerializeField] private AudioSource clickAudioSource;
    [SerializeField] private AudioClip clickClip;

    [SerializeField] private PauseMenu pauseMenu;

    void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (Input.GetKeyDown(KeyCode.P))
        {
            this.pauseMenu.Pause();
        }
    }

    public void PlayClickSFX()
    {
        if (clickAudioSource != null && clickClip != null)
        {
            clickAudioSource.PlayOneShot(clickClip);
        }
    }

    public void StopClickSFX()
    {
        if (clickAudioSource != null && clickAudioSource.isPlaying)
        {
            clickAudioSource.Stop();
        }
    }
}
