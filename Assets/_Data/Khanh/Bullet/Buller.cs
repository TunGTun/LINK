using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Animator animator;
    private bool isDestroying = false; // Đánh dấu trạng thái đạn đang biến mất

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isFlying", true); // Bật animation bay khi khởi tạo
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDestroying) return; // Nếu đang phát animation hủy thì bỏ qua

        if (other.CompareTag("Enemies")) // Nếu chạm vào kẻ địch
        {
            StartDestroy(); // Bắt đầu quá trình biến mất
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) // Nếu chạm vào mặt đất
        {
            StartDestroy(); // Bắt đầu quá trình biến mất
        }
    }

    void StartDestroy()
    {
        isDestroying = true;
        animator.SetBool("isFlying", false); // Tắt animation bay
        animator.SetTrigger("Destroy"); // Kích hoạt animation biến mất
        GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Dừng đạn lại
        GetComponent<Collider2D>().enabled = false; // Tắt collider để không va chạm nữa
    }

    // Hàm này sẽ được gọi từ **Animation Event** khi animation kết thúc
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
