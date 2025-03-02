using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MoveUpDown : MonoBehaviour
{
    public float speed = 0.1f; // Tốc độ di chuyển
    public float height = 0.1f; // Biên độ di chuyển
    public float delay = 1f; // Thời gian delay sau khi bắt đầu
    private float startY;
    private float startTime;

    void Start()
    {
        startY = transform.position.y;
        startTime = Time.time;
    }

    void Update()
    {
        if (Time.time >= startTime + delay)
        {
            float phase = Mathf.PingPong((Time.time - startTime - delay) * speed, height);
            float newY = startY + phase;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}
