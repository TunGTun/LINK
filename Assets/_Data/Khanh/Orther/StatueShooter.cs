using UnityEngine;

public class StatueShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shootInterval = 1f;
    public float bulletSpeed = 3f;
    public float bulletLifetime = 3f;

    private bool isShooting = false;
    private float lastShootTime = 0f;

    void Update()
    {
        if (isShooting && Time.time - lastShootTime >= shootInterval)
        {
            Shoot();
            lastShootTime = Time.time;

            // Kiểm tra nếu không còn Enemies thì dừng bắn
            if (EnemiesRemaining() == 0)
            {
                StopShooting();
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            randomDirection.y = Mathf.Abs(randomDirection.y) * -1; // Luôn đảm bảo Y âm

            rb.velocity = randomDirection * bulletSpeed;
        }


        Destroy(bullet, bulletLifetime);
    }

    public void StartShooting()
    {
        isShooting = true;
    }

    public void StopShooting()
    {
        isShooting = false;
    }

    public int enemyLayer; // Chọn layer của kẻ địch trong Inspector

    int EnemiesRemaining()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        int count = 0;

        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == enemyLayer)
            {
                count++;
            }
        }

        return count;
    }

}
