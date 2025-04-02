using UnityEngine;

public class StatueShooter : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab viên đạn
    public float shootInterval = 1f; // Thời gian giữa các lần bắn
    public float bulletSpeed = 3f; // Tốc độ bay của đạn
    public float bulletLifetime = 3f; // Thời gian tồn tại của đạn

    void Start()
    {
        InvokeRepeating(nameof(Shoot), 1f, shootInterval);
    }

    void Shoot()
    {
        // Tạo viên đạn tại vị trí của tượng
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized; // Hướng ngẫu nhiên
            rb.velocity = randomDirection * bulletSpeed; // Gán vận tốc cho đạn
        }

        // Tự hủy viên đạn sau bulletLifetime giây để tránh rác bộ nhớ
        Destroy(bullet, bulletLifetime);

    }
}
