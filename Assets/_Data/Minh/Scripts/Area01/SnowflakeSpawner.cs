using System.Collections;
using UnityEngine;

public class SnowflakeSpawner : MonoBehaviour
{
    public GameObject[] snowflakePrefabs; // Mảng chứa 12 Prefab bông tuyết
    public float spawnInterval = 1.5f; // Thời gian giữa các lần spawn
    public float spawnXMin = -5f, spawnXMax = 5f; // Khoảng spawn theo trục X
    public float spawnY = 6f; // Độ cao bông tuyết rơi

    private bool isSpawning = false;
    private Coroutine spawnRoutine;

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            spawnRoutine = StartCoroutine(SpawnSnowflakes());
        }
    }

    public void StopSpawning()
    {
        if (isSpawning)
        {
            isSpawning = false;
            if (spawnRoutine != null)
                StopCoroutine(spawnRoutine);
        }
    }

    IEnumerator SpawnSnowflakes()
    {
        while (isSpawning)
        {
            SpawnRandomSnowflake();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnRandomSnowflake()
    {
        if (snowflakePrefabs.Length == 0) return;

        int randomIndex = Random.Range(0, snowflakePrefabs.Length);
        GameObject randomSnowflake = snowflakePrefabs[randomIndex];

        float randomX = Random.Range(spawnXMin, spawnXMax); // Random vị trí X
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0); // Luôn spawn ở độ cao spawnY

        GameObject snowflake = Instantiate(randomSnowflake, spawnPosition, Quaternion.identity);
        snowflake.AddComponent<SnowflakeMovement>(); // Gán script di chuyển
    }
}
