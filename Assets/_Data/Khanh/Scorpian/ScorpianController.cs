using UnityEngine;
using System.Collections;

public class ScorpionController : MonoBehaviour
{
    public float speed = 2f;
    public float leftLimit = 76f;
    public float rightLimit = 90f;

    private bool movingRight = true;
    private bool isAttacking = false;
    private Animator animator;
    private Rigidbody2D rb;

    public GameObject attackZone;

    void Start()
    {
        attackZone.SetActive(false);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", true);
        animator.SetBool("isMoving", false);
        animator.SetTrigger("AttackTrigger");
        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(0.5f); // Đợi animation attack chạy xong

        EndAttack();
    }

    public void StartAttackZone()
    {
        attackZone.SetActive(true);
    }

    public void EndAttackZone()
    {
        attackZone.SetActive(false);
    }

    public void EndAttack()
    {
        isAttacking = false;
        animator.SetBool("isMoving", true);
        animator.SetBool("isAttacking", false);
        animator.ResetTrigger("AttackTrigger"); // Reset trigger để tránh bị kẹt animation
    }
}
