using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmNewGamePanel : LinkMonoBehaviour
{
    public GameObject confirmNewGamePanel;
    public RectTransform confirmNewGamePanelRect, newGameButtonRect, backButtonRect;
    public float rightPosX, middlePosX;
    public float topPosY, originalPosY;
    public float tweenDuration;
    public CanvasGroup canvasGroup;

    public void ConfirmNewGamePanelIntro()
    {
        confirmNewGamePanel.SetActive(true);
        canvasGroup.DOFade(1, tweenDuration).SetUpdate(true);
        confirmNewGamePanelRect.DOAnchorPosX(middlePosX, tweenDuration).SetUpdate(true);
        newGameButtonRect.DOAnchorPosY(topPosY, tweenDuration).SetUpdate(true);
        backButtonRect.DOAnchorPosY(topPosY, tweenDuration).SetUpdate(true);
    }

    public void ConfirmNewGamePanelOutro()
    {
        canvasGroup.DOFade(0, tweenDuration).SetUpdate(true);
        confirmNewGamePanelRect.DOAnchorPosX(rightPosX, tweenDuration).SetUpdate(true);
        newGameButtonRect.DOAnchorPosY(originalPosY, tweenDuration).SetUpdate(true);
        backButtonRect.DOAnchorPosY(originalPosY, tweenDuration).SetUpdate(true);
        StartCoroutine(WaitOutro());
    }

    IEnumerator WaitOutro()
    {
        yield return new WaitForSeconds(tweenDuration);
        confirmNewGamePanel.SetActive(false);
    }
}
