using UnityEngine;
using System.Collections;

public class FlowerShake : MonoBehaviour
{
    public float maxShakeAngle = 15f;  // Góc nghiêng tối đa (qua trái/phải)
    public float shakeSpeed = 5f;      // Tốc độ lắc
    public float shakeDecay = 0.98f;   // Hệ số giảm dần góc nghiêng
    public float minShakeAngle = 1f;   // Ngưỡng dừng lắc

    private Quaternion originalRotation;
    private float currentShakeAngle = 0f;
    private bool isShaking = false;

    private void Start()
    {
        originalRotation = transform.rotation; // Lưu góc quay ban đầu
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isShaking)
            {
                StartCoroutine(ShakeFlower());
            }
            else
            {
                currentShakeAngle = maxShakeAngle; // Nếu va chạm lại, reset góc nghiêng
            }
        }
    }

    IEnumerator ShakeFlower()
    {
        isShaking = true;
        currentShakeAngle = maxShakeAngle;

        while (currentShakeAngle > minShakeAngle)
        {
            float angle = Mathf.Sin(Time.time * shakeSpeed) * currentShakeAngle;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            // Giảm dần biên độ lắc theo thời gian
            currentShakeAngle *= shakeDecay;
            yield return null;
        }

        // Trả hoa về trạng thái ban đầu
        float time = 0f;
        while (time < 1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, time);
            time += Time.deltaTime * 3f;
            yield return null;
        }

        isShaking = false;
    }
}
