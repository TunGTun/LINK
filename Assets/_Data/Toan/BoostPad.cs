using System.Collections;
using UnityEngine;

public class BoostPad : MonoBehaviour
{
	public float boostMultiplier = 2f;  // Nhân tốc độ lên x2
	public float boostDuration = 3f;    // Thời gian hiệu ứng (giây)

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			CharMovement playerMovement = other.GetComponentInChildren<CharMovement>();

			if (playerMovement != null)
			{
				StartCoroutine(BoostSpeed(playerMovement));
				HideBoostPad(); // Ẩn BoostPad ngay lập tức
			}
		}
	}

	private IEnumerator BoostSpeed(CharMovement player)
	{
		if (player == null) yield break;

		float originalSpeed = player.GetMoveSpeed();
		player.SetMoveSpeed(originalSpeed * boostMultiplier);
		yield return new WaitForSeconds(boostDuration);

		if (player.GetMoveSpeed() == originalSpeed * boostMultiplier)
		{
			player.SetMoveSpeed(originalSpeed);
		}

		Destroy(gameObject);
	}

	private void HideBoostPad()
	{
		GetComponent<SpriteRenderer>().enabled = false; // Ẩn hình ảnh
		GetComponent<Collider2D>().enabled = false;    // Tắt va chạm
	}
}
