using UnityEngine;

public class ScorpionController : MonoBehaviour
{
    public float speed = 2f;
    public float leftLimit = 76f;
    public float rightLimit = 90f;

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
        if (other.CompareTag("Player"))
        {
            animator.SetBool("isAttacking", true);
            animator.SetBool("isMoving", false);
            animator.SetTrigger("AttackTrigger");
        }
    }


    // Gọi hàm này từ Animation Event sau khi Attack kết thúc
    public void EndAttack()
    {
        animator.SetBool("isAttacking", false);
        animator.SetBool("isMoving", true);
    }

}
