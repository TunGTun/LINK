using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    public float destroyYThreshold = 5f; // Giới hạn vị trí Y để hủy

    private void Update()
    {
        if (transform.position.y < destroyYThreshold)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemies"))
        {
            Destroy(collision.gameObject, 1f);
        }
        if (collision.CompareTag("Button"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("DeadZone"))
        {
            Destroy(collision.gameObject, 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemies"))
        {
            Destroy(collision.gameObject, 1f);
        }
        if (collision.collider.CompareTag("Button"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.collider.CompareTag("DeadZone"))
        {
            Destroy(collision.gameObject, 1f);
        }
    }
}
