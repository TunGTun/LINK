using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : LinkMonoBehaviour
{
    public GameObject optionMenu;

    public RectTransform optionPanelRect, mainMenu;
    public float leftPosX, middlePosX;
    public float tweenDuration;

    public RectTransform guidePanelRect, storyPanelRect, creditPanelRect;

    public void PlayGame()
    {
        SceneController.Instance.PlayGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Option()
    {
        optionMenu.SetActive(true);
        PanelIntro();
    }

    public void OptionCancel()
    {
        PanelOutro();
        StartCoroutine(WaitOutro());
    }

    IEnumerator WaitOutro()
    {
        yield return new WaitForSeconds(tweenDuration);
        optionMenu.SetActive(false);
    }

    void PanelIntro()
    {
        mainMenu.DOAnchorPosX(leftPosX, tweenDuration).SetUpdate(true);
        StartCoroutine(WaitHaftIntro());
    }

    IEnumerator WaitHaftIntro()
    {
        yield return new WaitForSeconds(tweenDuration);
        optionPanelRect.DOAnchorPosX(middlePosX, tweenDuration).SetUpdate(true);
    }

    void PanelOutro()
    {
        optionPanelRect.DOAnchorPosX(leftPosX, tweenDuration).SetUpdate(true);
        StartCoroutine(WaitHaftOutro());

    }

    IEnumerator WaitHaftOutro()
    {
        yield return new WaitForSeconds(tweenDuration);
        mainMenu.DOAnchorPosX(middlePosX, tweenDuration).SetUpdate(true);
    }

    public void Guide()
    {
        optionPanelRect.DOAnchorPosX(leftPosX, tweenDuration).SetUpdate(true);
        StartCoroutine(WaitHaftTransitionOutro(guidePanelRect));
    }

    public void Story()
    {
        optionPanelRect.DOAnchorPosX(leftPosX, tweenDuration).SetUpdate(true);
        StartCoroutine(WaitHaftTransitionOutro(storyPanelRect));
    }

    public void Credit()
    {
        optionPanelRect.DOAnchorPosX(leftPosX, tweenDuration).SetUpdate(true);
        StartCoroutine(WaitHaftTransitionOutro(creditPanelRect));
    }

    IEnumerator WaitHaftTransitionOutro(RectTransform rect)
    {
        yield return new WaitForSeconds(tweenDuration);
        rect.DOAnchorPosX(middlePosX - 452, tweenDuration).SetUpdate(true);
        StartCoroutine(WaitOutro());
    }

    public void GuideCancel()
    {
        optionMenu.SetActive(true);
        guidePanelRect.DOAnchorPosX(leftPosX - 452, tweenDuration).SetUpdate(true);
        StartCoroutine(WaitHaftIntro());
    }

    public void StoryCancel()
    {
        optionMenu.SetActive(true);
        storyPanelRect.DOAnchorPosX(leftPosX - 452, tweenDuration).SetUpdate(true);
        StartCoroutine(WaitHaftIntro());
    }

    public void CreditCancel()
    {
        optionMenu.SetActive(true);
        creditPanelRect.DOAnchorPosX(leftPosX - 452, tweenDuration).SetUpdate(true);
        StartCoroutine(WaitHaftIntro());
    }
}
