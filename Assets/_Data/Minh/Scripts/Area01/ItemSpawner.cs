using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;  // Object cần spawn
    public Transform player;          // Nhân vật để kiểm tra khoảng cách

    public float spawnDistance = 10f; // Khoảng cách để spawn
    public int maxObjects = 5;        // Số lượng object tối đa cùng lúc
    public float spawnInterval = 1f;  // Thời gian giữa mỗi lần spawn

    private List<GameObject> spawnedObjects = new List<GameObject>(); // Danh sách object đã spawn
    private bool isSpawning = false;  // Trạng thái spawn

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= spawnDistance && !isSpawning && spawnedObjects.Count < maxObjects)
        {
            StartCoroutine(SpawnObjects());
        }
        else if (distance > spawnDistance)
        {
            StopCoroutine(SpawnObjects());
            isSpawning = false;
        }

        CleanupDestroyedObjects();
    }

    IEnumerator SpawnObjects()
    {
        isSpawning = true;
        while (spawnedObjects.Count < maxObjects)
        {
            GameObject newObj = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
            spawnedObjects.Add(newObj);
            yield return new WaitForSeconds(spawnInterval);
        }
        isSpawning = false;
    }

    void CleanupDestroyedObjects()
    {
        spawnedObjects.RemoveAll(obj => obj == null);
    }
}
