using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeChase : LinkMonoBehaviour
{
    public float moveDistance = 0.8f;
    public float tiltAngle = 70f;
    public float tiltDuration = 0.2f;

    private bool isTriggered = false;

    public GameObject tFire;
    public GameObject cFire;
    public GameObject cSmoke;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTriggered || !other.CompareTag("Player")) return;

        Vector2 contactPoint = other.transform.position;
        Vector2 myPosition = transform.position;

        if (contactPoint.x < myPosition.x)
        {
            isTriggered = true;
            StartCoroutine(TiltAndMove());
        }
    }

    private System.Collections.IEnumerator TiltAndMove()
    {
        float elapsed = 0f;
        Quaternion startRot = transform.rotation;
        Quaternion targetRot = Quaternion.Euler(0f, 0f, -tiltAngle);

        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + Vector3.right * moveDistance;

        while (elapsed < tiltDuration)
        {
            float t = elapsed / tiltDuration;
            transform.rotation = Quaternion.Slerp(startRot, targetRot, t);
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRot;
        transform.position = targetPos;

        cFire.transform.DOScale(Vector3.one, 0.5f);

        yield return new WaitForSeconds(0.5f);
        cSmoke.transform.DOMoveY(-1.75f, 2f);
        tFire.transform.DOScale(Vector3.zero, 0.5f);
    }
}
