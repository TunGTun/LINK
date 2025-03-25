using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rino : LinkMonoBehaviour
{
    public Transform player; // Gán Player vào trong Inspector
    public float speed = 3f; // Tốc độ di chuyển
    public float detectionRangeX = 5f; // Phạm vi phát hiện theo trục X
    public float sizeY;
    private Vector2 moveDirection;
    private bool shouldMove = false;
    public float checkInterval = 2f; // Thời gian giữa các lần kiểm tra vị trí Player
    private float checkTimer;

    protected override void Start()
    {
        base.Start();
        sizeY = GetComponent<Collider2D>().bounds.size.y; // Lấy kích thước Y của GameObject
        checkTimer = checkInterval;
    }

    void Update()
    {
        if (player == null) return;

        checkTimer -= Time.deltaTime;
        if (checkTimer <= 0)
        {
            CheckPlayerPosition();
            checkTimer = checkInterval;
        }

        // Nếu đã xác định hướng, tiếp tục di chuyển thẳng
        if (shouldMove)
        {
            transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
        }
    }

    private void CheckPlayerPosition()
    {
        Vector2 enemyPos = transform.position;
        Vector2 playerPos = player.position;

        float distanceX = Mathf.Abs(playerPos.x - enemyPos.x);
        float distanceY = Mathf.Abs(playerPos.y - enemyPos.y);

        // Kiểm tra điều kiện về khoảng cách và chiều cao
        if (distanceX < detectionRangeX && distanceY < sizeY / 2)
        {
            // Xác định hướng di chuyển về phía Player
            moveDirection = (playerPos - enemyPos).normalized;
            moveDirection.y = 0; // Không di chuyển theo trục Y
            shouldMove = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            shouldMove = false; // Dừng di chuyển khi chạm vào tường
        }
    }
}
