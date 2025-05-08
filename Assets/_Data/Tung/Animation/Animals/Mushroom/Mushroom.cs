using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public float forceMagnitude = 10f;
    public Animator animator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.transform.position.y > transform.position.y)
            {
                Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    playerRb.velocity = Vector2.zero;
                    animator.SetTrigger("isBounce");
                    playerRb.AddForce(Vector2.up * forceMagnitude, ForceMode2D.Impulse);
                }
            }
        }
    }
}
