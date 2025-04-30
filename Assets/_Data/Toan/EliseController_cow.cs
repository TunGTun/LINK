using System.Collections;
using UnityEngine;

public class EliseController_cow : MonoBehaviour
{
	public float speed = 2f;
	public GameObject attackZone;

	private bool movingRight = true;
	private bool isAttacking = false;

	private Animator animator;
	private Rigidbody2D rb;

	void Start()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		if (attackZone != null)
		{
			attackZone.SetActive(false);
		}
	}

	void Update()
	{
		if (!isAttacking)
		{
			Move();
			CheckWallAhead(); // <-- Thêm hàm này
		}
	}


	void Move()
	{
		animator.SetBool("isMoving", true);

		float moveDirection = movingRight ? 1 : -1;
		rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
	}

	void Flip()
	{
		// Lật hướng hiển thị
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		// Lấy GameObject chính của đối tượng va chạm
		GameObject other = collision.collider.gameObject;

		// Dùng tag của GameObject gốc
		if (other.CompareTag("Wall"))
		{
			Debug.Log("Đã chạm tường! Quay đầu lại.");
			movingRight = !movingRight;
			Flip();
		}
	}
	void CheckWallAhead()
	{
		float direction = movingRight ? 1f : -1f;
		Vector2 origin = transform.position;
		Vector2 rayDirection = new Vector2(direction, 0f);

		// Cast ray một đoạn nhỏ phía trước để phát hiện tường
		RaycastHit2D hit = Physics2D.Raycast(origin, rayDirection, 0.6f, LayerMask.GetMask("Default"));

		if (hit.collider != null && hit.collider.CompareTag("Wall"))
		{
			Debug.Log("Raycast gặp tường → quay đầu");
			movingRight = !movingRight;
			Flip();
		}
	}



	IEnumerator AttackRoutine()
	{
		isAttacking = true;

		rb.velocity = Vector2.zero;
		animator.SetBool("isMoving", false);
		animator.SetBool("isAttacking", true);
		animator.SetTrigger("AttackTrigger");

		yield return new WaitForSeconds(0.5f);

		EndAttack();
	}

	public void StartAttackZone() => attackZone?.SetActive(true);

	public void EndAttackZone() => attackZone?.SetActive(false);

	public void EndAttack()
	{
		isAttacking = false;
		animator.SetBool("isAttacking", false);
		animator.SetBool("isMoving", true);
		animator.ResetTrigger("AttackTrigger");
	}
}
