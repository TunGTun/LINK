using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBridge : LinkMonoBehaviour
{
    public float timeBeforeShake = 0f; // Thời gian trước khi rung
    public float timeBeforeFall = 2f; // Thời gian trước khi rơi
    public float shakeIntensity = 0.05f; // Độ rung
    public float shakeDuration = 2f; // Thời gian rung
    public float respawnDelay = 3f;

    private Vector3 originalPosition;
    private Rigidbody2D rb;
    private bool isActivated = false;

    protected override void Start()
    {
        base.Start();
        originalPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // Ngăn cầu rơi ngay từ đầu
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isActivated && collision.collider.CompareTag("Player")) // Chỉ kích hoạt một lần
        {
            isActivated = true;
            StartCoroutine(ShakeAndFall());
        }
    }

    IEnumerator ShakeAndFall()
    {
        yield return new WaitForSeconds(timeBeforeShake);

        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            transform.position = originalPosition + (Vector3)Random.insideUnitCircle * shakeIntensity;
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
        yield return new WaitForSeconds(timeBeforeFall - shakeDuration);

        rb.isKinematic = false; // Cho phép rơi xuống
        rb.gravityScale = 1f; // Đặt trọng lực để cầu rơi
        yield return new WaitForSeconds(respawnDelay);

        transform.localScale = Vector3.zero;
        transform.position = originalPosition;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;
        isActivated = false;
        transform.DOScale(Vector3.one, 1f);
    }
}
