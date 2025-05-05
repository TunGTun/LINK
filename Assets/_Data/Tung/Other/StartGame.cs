using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : LinkMonoBehaviour
{
    [SerializeField] protected GameObject player;
    [SerializeField] protected Rigidbody2D rigid2D;

    protected override void Start()
    {
        base.Start();
        player.transform.localScale = Vector3.zero;
        player.transform.position = new Vector3(1, -1, -5);
        rigid2D.simulated = false;
        InputManager.Instance.InputAllowed = false;
        StartCoroutine(WaitForAnim());
    }

    IEnumerator WaitForAnim()
    {
        yield return new WaitForSeconds(1f);
        player.transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 1f);
        yield return new WaitForSeconds(1f);
        rigid2D.simulated = true;
        InputManager.Instance.InputAllowed = true;
    }

}
