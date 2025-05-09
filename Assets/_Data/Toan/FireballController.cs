using UnityEngine;

public class FireballController : MonoBehaviour
{
	[Header("Fireball Settings")]
	public Transform player;   // Tham chiếu đến nhân vật
	public float fallSpeed = 40f;  // Tốc độ rơi của quả cầu lửa


	private bool isFalling = false; // Trạng thái rơi
	private SpriteRenderer spriteRenderer; // Điều khiển hiển thị hình ảnh

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>(); // Lấy SpriteRenderer


		if (player == null)
		{
			player = GameObject.FindGameObjectWithTag("Player")?.transform;
			if (player == null) Debug.LogError("🔥 FireballController: Không tìm thấy Player! Hãy gán trong Inspector.");
		}
	}

	private void Update()
	{
		if (!isFalling)
		{
			// Kiểm tra nếu nhân vật đi qua vị trí trigger và Y thấp hơn quả cầu lửa
			if (player.position.x > transform.position.x - 1 && player.position.y <= transform.position.y)
			{
				isFalling = true; // Kích hoạt rơi
			}
		}
		else
		{
			// Quả cầu lửa rơi xuống
			transform.position += Vector3.down * fallSpeed * Time.deltaTime;
		}

		if (transform.position.y < -5.11)
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision == null)
		{
			return;
		}

		// Kiểm tra xem đối tượng có tag "Player" không
		if (!collision.CompareTag("Player")) gameObject.SetActive(false);



	}
}
