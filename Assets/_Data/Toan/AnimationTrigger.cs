using UnityEngine;

public class FireballAnimationTrigger : MonoBehaviour
{
	private Animator animator; // Biến chứa Animator

	private void Start()
	{
		// Lấy Animator từ GameObject
		animator = GetComponent<Animator>();
		if (animator == null)
		{
			Debug.LogError("🔥 FireballAnimationTrigger: Không tìm thấy Animator!");
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{

		if (collision.CompareTag("Player"))
		{
			Debug.Log("🔥 Kích hoạt animation!");
			animator.SetTrigger("Activate"); // Kích hoạt animation
		}
	}
}
