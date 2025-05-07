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

    public GameObject beePrefab;      // Prefab của ong
    public GameObject beeHolder;   // GameObject cha để chứa các ong
    public int beeCount = 5;          // Số lượng ong cần spawn
    public float spawnInterval = 2f; // Thời gian giữa các lần spawn


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

        yield return new WaitForSeconds(2f);
        StartCoroutine(SpawnBees());

    }

    private IEnumerator SpawnBees()
    {
        for (int i = 0; i < beeCount; i++)
        {
            float randomPosition = Random.Range(-1f, 1f);
            Vector3 beePosition = new Vector3(
                beeHolder.transform.position.x + randomPosition,
                beeHolder.transform.position.y + randomPosition,
                0f // Đảm bảo z = 0
            );

            GameObject bee = Instantiate(beePrefab, beePosition, Quaternion.identity, beeHolder.transform);

            Vector3 fixedPosition = bee.transform.localPosition;
            fixedPosition.z = 0f;
            bee.transform.localPosition = fixedPosition;

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
