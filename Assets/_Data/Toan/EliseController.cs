using UnityEngine;

public class EliseController : MonoBehaviour
{
	[SerializeField] private float speed = 5f;
	private Vector2 direction = Vector2.up;

	private void Update()
	{
		MoveCharacter();
	}

	private void MoveCharacter()
	{
		transform.Translate(direction * speed * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// Khi chạm vào tường, đổi hướng di chuyển
		if (collision.CompareTag("Wall"))
		{
			direction *= -1; // Đảo chiều
		}
	}
}
