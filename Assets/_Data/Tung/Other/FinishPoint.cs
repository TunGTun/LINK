using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : LinkMonoBehaviour
{
    [SerializeField] protected GameObject player;
    [SerializeField] protected Rigidbody2D rigid2D;
    [SerializeField] protected Transform finishPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.UnlockNewLevel();
            rigid2D.simulated = false;
            InputManager.Instance.InputAllowed = false;
            player.transform.DOScale(Vector3.zero, 1f);
            player.transform.DOMove(new Vector3(finishPoint.position.x, finishPoint.position.y, -5), 1f);
            StartCoroutine(WaitForNextLevel());
        }
    }

    IEnumerator WaitForNextLevel()
    {
        yield return new WaitForSeconds(1f);
        SceneController.Instance.NextLevel();
    }

    protected virtual void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
}
