using UnityEngine;

public class BounceController : MonoBehaviour
{
    public float bounceSpeed = 2f;         // Tốc độ nhún lên xuống
    public float bounceHeight = 0.5f;      // Biên độ nhún
    public float activeDistance = 5f;      // Khoảng cách kích hoạt nhún

    private Vector3 initialPosition;
    private Transform player;

    void Start()
    {
        initialPosition = transform.position;

        // Tìm player theo tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Không tìm thấy GameObject có tag 'Player'");
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= activeDistance)
        {
            float newY = initialPosition.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
            transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
        }
        else
        {
            transform.position = initialPosition; // Quay về vị trí ban đầu
        }
    }
}
