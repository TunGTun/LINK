using UnityEngine;
using System.Collections;

public class MoveToNextArea2D : MonoBehaviour
{
    public Vector3 targetPosition;  // Vị trí đích
    public float moveSpeed = 2f;    // Tốc độ di chuyển
    public float delayTime = 1f;    // Thời gian dừng lại khi đến nơi

    private Vector3 startPosition;  // Lưu vị trí ban đầu
    private bool movingToTarget = true; // Kiểm tra hướng di chuyển
    private bool hasStarted = false; // Kiểm tra xem đã bắt đầu di chuyển chưa

    void Start()
    {
        startPosition = transform.position; // Lưu lại vị trí ban đầu
    }

    void Update()
    {
        if (hasStarted)
        {
            Vector3 target = movingToTarget ? targetPosition : startPosition;
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

            // Xoay hướng về phía target
            FlipCharacter(target);

            if (Vector3.Distance(transform.position, target) < 0.01f) // Khi đến nơi, đổi hướng
            {
                StartCoroutine(SwitchDirectionAfterDelay());
            }
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasStarted)
        {
            hasStarted = true; // Bắt đầu di chuyển
        }
    }

    IEnumerator SwitchDirectionAfterDelay()
    {
        hasStarted = false; // Tạm dừng di chuyển
        yield return new WaitForSeconds(delayTime); // Đợi thời gian delay
        movingToTarget = !movingToTarget; // Đổi hướng
        hasStarted = true; // Tiếp tục di chuyển
    }

    void FlipCharacter(Vector3 target)
    {
        Vector3 scale = transform.localScale;
        if ((target.x < transform.position.x && scale.x > 0) || (target.x > transform.position.x && scale.x < 0))
        {
            scale.x *= -1; // Đảo hướng xoay
        }
        transform.localScale = scale;
    }
}
