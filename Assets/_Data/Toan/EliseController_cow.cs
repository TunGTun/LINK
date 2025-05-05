using UnityEngine;

public class EliseController_cow : MonoBehaviour
{
	private Vector2 direction = Vector2.right;
	[SerializeField] private float speed = 5f;

	private void Update()
	{
		MoveCharacter();
		FlipSprite();
	}

	private void MoveCharacter()
	{
		transform.Translate(direction * speed * Time.deltaTime);
	}

	private void FlipSprite()
	{
		if (direction.x != 0)
		{
			GetComponent<SpriteRenderer>().flipX = direction.x < 0;
		}
	}


	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Wall"))
		{
			direction *= -1; // Đảo chiều khi va tường
		}
	}


}
