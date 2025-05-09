using UnityEngine;

public class fireball2 : MonoBehaviour
{
	public Transform player;   // Tham chiếu đến nhân vật
	public float fallSpeed = 10f;  // Tốc độ rơi của quả cầu lửa
	public double posiony = 7.6;

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
			if (player.position.x <= transform.position.x + 2 - 1 && player.position.y <= transform.position.y && player.position.y > posiony)
			{
				isFalling = true;

			}
		}
		else
		{
			// Quả cầu lửa rơi xuống
			transform.position += Vector3.down * fallSpeed * Time.deltaTime;
		}

		if (transform.position.y < 8.2)
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision == null)
		{
			Debug.LogError("🔥 FireballController: collision is NULL!");
			return;
		}


		if (!collision.CompareTag("Player")) return;


		Destroy(gameObject);
	}
}
