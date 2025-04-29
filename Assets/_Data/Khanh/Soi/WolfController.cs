using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfController : MonoBehaviour
{
    public float speed = 2f;
    public float leftLimit = 270f;
    public float rightLimit = 290f;
    public GameObject AttackZone;

    private bool movingRight = true;
    private bool isAttacking = false;
    private bool isDead = false;
    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        AttackZone.SetActive(false);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isAttacking&&!isDead)
        {
            Move();
        }
    }

    void Move()
    {
        animator.SetBool("isMoving", true);

        if (movingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (transform.position.x >= rightLimit)
            {
                Flip();
                movingRight = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (transform.position.x <= leftLimit)
            {
                Flip();
                movingRight = true;
            }
        }
    }

    void Flip()
    {
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
        if (other.CompareTag("Wheel"))
        {
            StartCoroutine(Dead());
        }
    }

    IEnumerator Dead()
    {
        isDead = true;
        animator.SetTrigger("DieTrigger");
        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(1f);

        dead();
    }

    public void dead()
    {
        Destroy(gameObject);
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", true);
        animator.SetBool("isMoving", false);
        animator.SetTrigger("AttackTrigger");
        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(1f); // Đợi animation attack chạy xong

        EndAttack();
    }

    public void StartAttackZone()
    {
        AttackZone.SetActive(true);
    }

    public void EndAttackZone()
    {
        AttackZone.SetActive(false);
    }

    public void EndAttack()
    {
        isAttacking = false;
        animator.SetBool("isMoving", true);
        animator.SetBool("isAttacking", false);
        animator.ResetTrigger("AttackTrigger"); // Reset trigger để tránh bị kẹt animation
    }
}