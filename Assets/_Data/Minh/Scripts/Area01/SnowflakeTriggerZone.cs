using UnityEngine;

public class SnowflakeTriggerZone : MonoBehaviour
{
    public SnowflakeSpawner spawner; // Tham chiếu đến nơi rơi bông tuyết

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Kiểm tra nếu nhân vật đi vào
        {
            spawner.StartSpawning();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Kiểm tra nếu nhân vật đi ra
        {
            spawner.StopSpawning();
        }
    }
}
