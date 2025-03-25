using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOn : LinkMonoBehaviour
{
    public Transform player; // Nhân vật
    public float triggerDistance = 5f; // Khoảng cách để bắt đầu phóng to
    public Vector3 maxScale = new Vector3(2f, 2f, 2f); // Kích thước lớn nhất
    public float scaleSpeed = 1.5f; // Tốc độ phóng to
    public float overshootFactor = 1f; // Hệ số nảy (phóng to vượt maxScale)
    private Vector3 overshootScale; // Kích thước vượt ngưỡng

    private bool isScalingUp = false; // Đánh dấu khi đang phóng to

    protected override void Start()
    {
        base.Start();
        overshootScale = maxScale * overshootFactor; // Tính toán kích thước nảy
    }

    void Update()
    {
        float distance = Mathf.Abs(transform.position.x - player.position.x);

        if (distance < triggerDistance)
        {
            if (!isScalingUp)
            {
                StartCoroutine(ScaleUpAndDown());
                isScalingUp = true;
            }
        }
    }

    IEnumerator ScaleUpAndDown()
    {
        // Phóng to quá maxScale
        while (Vector3.Distance(transform.localScale, overshootScale) > 0.01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, overshootScale, Time.deltaTime * scaleSpeed);
            yield return null;
        }

        // Thu nhỏ về maxScale
        while (Vector3.Distance(transform.localScale, maxScale) > 0.01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, maxScale, Time.deltaTime * scaleSpeed * 3);
            yield return null;
        }

        transform.localScale = maxScale; // Đảm bảo dừng đúng maxScale
    }
}
