using System;
using System.Collections;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    public static event Action onSkeletonRisen;
    public GameObject attackZone;

    public float speed = 2f;
    public float leftLimit = 120f;
    public float rightLimit = 160f;
    public float riseHeight = 8f; // Độ cao bộ xương nhô lên
    public float riseSpeed = 1.5f; // Tốc độ nhô lên

    private bool movingRight = true;
    private bool isAttacking = false;
    private bool isDead = false;
    private bool isRising = false;

    private Animator animator;
    private Rigidbody2D rb;
    private Vector3 startPosition; // Vị trí ban đầu (dưới đất)

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        attackZone.SetActive(false);

        // Đặt bộ xương ở dưới đất khi game bắt đầu
        transform.position = new Vector3(startPosition.x, startPosition.y - riseHeight, startPosition.z);
        rb.velocity = Vector2.zero;
        rb.isKinematic = true; // Không bị ảnh hưởng bởi trọng lực
    }

    void Update()
    {
        if (!isAttacking && !isDead && isRising)
        {
            Move();
        }
    }

    void Move()
    {
        animator.SetBool("isMoving", true);
        animator.SetTrigger("Rise");

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
            StartAttack();
        }

        if (other.CompareTag("Bullet"))
        {
            Die();
        }
    }

    IEnumerator RiseFromGround()
    {
        isRising = true;
        float elapsedTime = 0f;
        Vector3 targetPosition = startPosition;

        while (elapsedTime < riseSpeed)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, (elapsedTime / riseSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        onSkeletonRisen?.Invoke();
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;
        animator.SetTrigger("DieTrigger");
        rb.velocity = Vector2.zero;
    }

    public void dead()
    {
        Destroy(gameObject);
    }
    public void ActivateSkeleton()
    {
        if (!isRising)
        {
            StartCoroutine(RiseFromGround());
        }
    }

    public void StartAttackZone()
    {
        attackZone.SetActive(true);
    }

    public void EndAttackZone()
    {
        attackZone.SetActive(false);
    }

    public void StartAttack()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", true);
        animator.SetBool("isMoving", false);
        animator.SetTrigger("AttackTrigger");
        rb.velocity = Vector2.zero;
    }

    public void EndAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
        animator.SetBool("isMoving", true);
    }
}
