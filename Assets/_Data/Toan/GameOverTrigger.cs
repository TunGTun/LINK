using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Destroy(other.gameObject); // Hủy nhân vật

		}
	}
}

