using System.Collections;
using UnityEngine;

public class ShakeAndRotate2D : MonoBehaviour
{
    public float tiltAngle = -2f;    // Góc nghiêng khi va chạm 
    public int maxHits = 3;          // Số lần va chạm trước khi xoay
    public float rotateAngle = 0f;  // Góc xoay sau khi đạt số lần va chạm 
    public float shakeIntensity = 0.02f; // Cường độ rung
    public float shakeDuration = 0.2f;  // Thời gian rung
    public float rotationDuration = 1f; // Thời gian nghiêng dần về rotateAngle
    public GameObject ground;

    private int hitCount = 0;
    private float currRotation;
    private bool stopChecking = false;
    private Vector3 originalPosition;

    void Start()
    {
        currRotation = transform.rotation.eulerAngles.z;
        originalPosition = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (stopChecking) return;

        if (other.CompareTag("Enemies"))
        {
            hitCount++;
            StartCoroutine(ShakeAndTilt());
        }
    }

    IEnumerator ShakeAndTilt()
    {
        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-shakeIntensity, shakeIntensity), Random.Range(-shakeIntensity, shakeIntensity), 0);
            transform.position = originalPosition + randomOffset;
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPosition;

        if (hitCount < maxHits)
        {
            currRotation += tiltAngle;
            transform.rotation = Quaternion.Euler(0, 0, currRotation);
        }
        else
        {
            yield return StartCoroutine(RotateSmoothlyAccelerated(currRotation, rotateAngle, rotationDuration));
            stopChecking = true; // Ngừng kiểm tra va chạm sau khi đạt maxHits
        }
    }

    IEnumerator RotateSmoothlyAccelerated(float fromAngle, float toAngle, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float newAngle = Mathf.Lerp(fromAngle, toAngle, t * t); // Tăng tốc dần
            transform.rotation = Quaternion.Euler(0, 0, newAngle);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0, 0, toAngle);
        ground.SetActive(true);
    }
}
