using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBounce2D : LinkMonoBehaviour
{
    public float dropAmount = 0.15f; // Khoảng cách hạ xuống
    public float duration = 0.2f;   // Thời gian hạ xuống và phục hồi

    private Vector3 originalPosition;
    private bool isDropping = false; // Kiểm soát trạng thái hạ xuống

    protected override void Start()
    {
        base.Start();
        originalPosition = transform.position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDropping && collision.gameObject.CompareTag("Player"))
        {
            ContactPoint2D contact = collision.contacts[0];
            if (contact.normal.y < -0.5f) // Chỉ kích hoạt nếu va chạm từ trên xuống
            {
                isDropping = true; // Đánh dấu đang hạ xuống
                LeanTween.moveY(gameObject, originalPosition.y - dropAmount, duration)
                    .setEase(LeanTweenType.easeOutQuad)
                    .setOnComplete(() =>
                    {
                        LeanTween.moveY(gameObject, originalPosition.y, duration)
                            .setEase(LeanTweenType.easeOutBounce)
                            .setOnComplete(() => isDropping = false); // Đặt lại trạng thái khi hoàn thành
                    });
            }
        }
    }
}
