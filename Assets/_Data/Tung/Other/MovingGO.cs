using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGO : MonoBehaviour
{
    public float minY; // Giá trị m
    public float maxY;  // Giá trị n
    public float minDuration;
    public float maxDuration; // Thời gian di chuyển
    public float delayTime; // Thời gian di chuyển

    private float targetX;

    void Start()
    {
        // Chọn vị trí spawn ngẫu nhiên trong khoảng [m, n]
        float spawnY = Random.Range(minY, maxY);
        transform.position = new Vector3(transform.position.x, spawnY, transform.position.z);

        // Xác định vị trí đích
        targetX = -transform.position.x;

        StartCoroutine(StartWithDelay(delayTime));
    }

    private IEnumerator StartWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(MoveToTarget());
    }

    private IEnumerator MoveToTarget()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(targetX, startPosition.y, startPosition.z);
        float elapsedTime = 0f;

        float currentDuration = Random.Range(minDuration, maxDuration);
        while (elapsedTime < currentDuration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / currentDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;
        float newY = Random.Range(minY, maxY);
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

        StartCoroutine(MoveToTarget());
    }
}
