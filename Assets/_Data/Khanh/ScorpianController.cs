using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpianController : MonoBehaviour
{
    public float speed = 2f; // T?c ð? di chuy?n
    public float leftLimit = -3f; // Gi?i h?n bên trái
    public float rightLimit = 3f; // Gi?i h?n bên ph?i

    private bool movingRight = true; // Tr?ng thái di chuy?n
    private bool isAttacking = false; // Tr?ng thái t?n công
    private Animator animator;
    private Transform player; // Tham chi?u ð?n nhân v?t

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isAttacking)
        {
            Move();
        }
    }

    void Move()
    {
        // Di chuy?n trái/ph?i
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (transform.position.x >= rightLimit)
            {
                Flip();
                movingRight = false;
            }
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (transform.position.x <= leftLimit)
            {
                Flip();
                movingRight = true;
            }
        }

        animator.SetBool("isMoving", true); // Chuy?n animation sang Move
    }

    void Flip()
    {
        // L?t hý?ng b? c?p
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isAttacking = true;
            animator.SetTrigger("AttackTrigger"); // Chuy?n sang animation Attack
        }
    }

    // Hàm này có th? g?i t? animation event ð? reset sau khi t?n công
    public void EndAttack()
    {
        isAttacking = false;
        animator.SetBool("isMoving", true); // Ti?p t?c di chuy?n
    }
}
