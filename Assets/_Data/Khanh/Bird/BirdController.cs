using UnityEngine;

public class BirdController : MonoBehaviour
{
    public Transform targetPoint;  // Điểm đến của chim
    public float moveSpeed = 2f;   // Tốc độ bay của chim
    private Transform player;      // Nhân vật
    private bool isFlying = false; // Kiểm tra xem chim có đang bay không
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Khi nhân vật chạm vào vùng đậu trên lưng chim
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isFlying)
        {
            player = other.transform;
            player.SetParent(transform);  // Gắn nhân vật vào chim để di chuyển cùng
            isFlying = true;
            anim.SetTrigger("Move"); // Chuyển sang animation bay
        }
    }

    void Update()
    {
        if (isFlying)
        {
            // Di chuyển chim đến điểm chỉ định
            transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

            // Khi đến nơi, dừng lại
            if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
            {
                isFlying = false;
                player.SetParent(null); // Thả nhân vật xuống
                anim.SetTrigger("Idle"); // Quay lại animation chờ
            }
        }
    }
}
