using UnityEngine;

public class SnowflakeMovement : MonoBehaviour
{
    public float fallSpeed = 1.5f; // Tốc độ rơi
    public float swayAmount = 1f; // Độ dao động ngang
    public float swaySpeed = 2f; // Tốc độ dao động
    public float destroyY = -5f; // Vị trí y quá thấp thì hủy

    private float startX;

    void Start()
    {
        startX = transform.position.x;
    }

    void Update()
    {
        // Rơi xuống từ từ
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        // Dao động nhẹ nhàng trái/phải
        float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
        transform.position = new Vector3(startX + sway, transform.position.y, transform.position.z);

        // Kiểm tra nếu rơi quá thấp thì hủy
        if (transform.position.y < destroyY)
        {
            Destroy(gameObject);
        }
    }
}
