using UnityEngine;

public class SnowflakeMovement : MonoBehaviour
{
    private float fallSpeed;  // Tốc độ rơi (random)
    private float swayAmount; // Độ lắc lư (random)
    private float swaySpeed;  // Tốc độ lắc lư (random)
    private float startX;     // Vị trí X ban đầu
    private float swayOffset; // Độ lệch pha (để lắc lư khác nhau)

    public float destroyY = -5f; // Khi rơi xuống quá thấp sẽ bị hủy

    void Start()
    {
        startX = transform.position.x;

        // Random tốc độ rơi
        fallSpeed = Random.Range(1.2f, 2.5f);

        // Random biên độ lắc lư (dao động trái/phải)
        swayAmount = Random.Range(0.3f, 1.5f);

        // Random tốc độ lắc lư
        swaySpeed = Random.Range(1f, 3f);

        // Random pha để mỗi bông tuyết lắc lư khác nhau
        swayOffset = Random.Range(0f, Mathf.PI * 2);

        Debug.Log($"❄️ Tuyết {gameObject.name} | FallSpeed: {fallSpeed}, Sway: {swayAmount}, Speed: {swaySpeed}");
    }

    void Update()
    {
        // Rơi xuống
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        // Lắc lư trái/phải ngẫu nhiên
        float sway = Mathf.Sin(Time.time * swaySpeed + swayOffset) * swayAmount;
        transform.position = new Vector3(startX + sway, transform.position.y, transform.position.z);

        // Hủy nếu rơi quá thấp
        if (transform.position.y < destroyY)
        {
            Destroy(gameObject);
        }
    }
}
