using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadController : MonoBehaviour
{
    public float bounce = 20f;
    public Animator animator;
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collider2D playerCollider = collision.collider;
            Collider2D jumpPadCollider = GetComponent<Collider2D>();


            float playerBottom = playerCollider.bounds.min.y;
            float jumpPadTop = jumpPadCollider.bounds.max.y;

            if (playerBottom >= jumpPadTop)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
                animator.SetBool("isPadding", true);
                StartCoroutine(WaitForPadding());
            }

        }
    }
    IEnumerator WaitForPadding()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("isPadding", false);
    }

}
