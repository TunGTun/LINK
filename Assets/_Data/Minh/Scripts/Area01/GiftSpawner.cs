using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public float minY = -5f; // Giá trị y tối thiểu
    public GameObject prefab; // Prefab của vật thể
    private Vector3 startPosition;
    private bool hasSpawned = false; // Kiểm soát chỉ spawn 1 lần

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (!hasSpawned && transform.position.y < minY)
        {
            Instantiate(prefab, startPosition, Quaternion.identity);
            hasSpawned = true;
        }
    }

}
