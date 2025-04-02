using UnityEngine;

public class EliseController_cow : MonoBehaviour
{
    public float speed = 2f;
    private bool movingRight = true;
    private bool isAttacking = false;

    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
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
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }

    void Flip()
    {
        movingRight = !movingRight; // Đảo hướng
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("isAttacking", true);
            animator.SetBool("isMoving", false);
            animator.SetTrigger("AttackTrigger");
            isAttacking = true;
        }
        else if (other.CompareTag("Wall")) // Khi chạm tường, quay lại
        {
            Flip();
        }
    }

    public void EndAttack()
    {
        animator.SetBool("isAttacking", false);
        animator.SetBool("isMoving", true);
        isAttacking = false;
    }
}


