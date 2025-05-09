using UnityEngine;
using System.Collections.Generic;

public class BoxSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;  // Prefab dùng để spawn thêm
    public int maxObjects = 10;       // Tổng số lượng tối đa object tồn tại
    public float minY = -5f;          // Ngưỡng Y để kiểm tra
    private List<GameObject> trackedObjects = new List<GameObject>();
    private List<Vector3> originalPositions = new List<Vector3>();

    void Start()
    {
        // Tìm và lưu tất cả các object có sẵn trong scene với tag "Spawned"
        GameObject[] existingObjects = GameObject.FindGameObjectsWithTag("Spawned");
        foreach (GameObject obj in existingObjects)
        {
            trackedObjects.Add(obj);
            originalPositions.Add(obj.transform.position);
        }
    }

    void Update()
    {
        for (int i = 0; i < trackedObjects.Count; i++)
        {
            GameObject obj = trackedObjects[i];
            if (obj == null) continue;

            if (obj.transform.position.y < minY)
            {
                Destroy(obj); // Xoá object cũ

                // Spawn mới ở vị trí ban đầu nếu chưa vượt quá giới hạn
                if (trackedObjects.Count < maxObjects)
                {
                    GameObject newObj = Instantiate(objectToSpawn, originalPositions[i], Quaternion.identity);
                    trackedObjects[i] = newObj;
                }
            }
        }
    }
}
