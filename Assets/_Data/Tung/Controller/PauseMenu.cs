using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PauseMenu : LinkMonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public RectTransform pausePanelRect, pauseButtonRect, timerText;
    public float topPosY, middlePosY;
    public float tweenDuration;
    public CanvasGroup canvasGroup;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        PausePanelIntro();
    }

    public void Home()
    {
        SceneController.Instance.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public async void Resume()
    {
        await PausePanelOutro();
        pauseMenu.SetActive(false);
        Time.timeScale = 1;

    }

    public void Restart()
    {
        SceneController.Instance.LoadScene(SceneController.Instance.GetActiveSceneName());
        Time.timeScale = 1;
    }

    void PausePanelIntro()
    {
        canvasGroup.DOFade(1, tweenDuration).SetUpdate(true);
        pausePanelRect.DOAnchorPosY(middlePosY, tweenDuration).SetUpdate(true);
        pauseButtonRect.DOAnchorPosX(65, tweenDuration).SetUpdate(true);
        timerText.DOAnchorPosY(40, tweenDuration).SetUpdate(true);
    }

    async Task PausePanelOutro()
    {
        canvasGroup.DOFade(0, tweenDuration).SetUpdate(true);
        await pausePanelRect.DOAnchorPosY(topPosY, tweenDuration).SetUpdate(true).AsyncWaitForCompletion();
        pauseButtonRect.DOAnchorPosX(-65, tweenDuration).SetUpdate(true);
        timerText.DOAnchorPosY(-40, tweenDuration).SetUpdate(true);
    }
}
