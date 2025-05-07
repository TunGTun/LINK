using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Bee : MonoBehaviour
{
    public float attackCooldown = 1f;
    public float attackRangeMin = 265.5f;
    public float attackRangeMax = 308.5f;
    public float attackSpeed = 12f;
    public float waitAfterHitGround = 0.3f;
    public float returnHeight = 3.5f;

    private Transform player;
    private float cooldownTimer;
    private bool isAttacking = false;
    private bool shouldMove = false;
    private Vector2 moveDirection;
    private bool isReturningToAir = false;
    private float originalScaleX;
    private Quaternion originalRotation;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        cooldownTimer = 2f;
        originalScaleX = transform.localScale.x;
        originalRotation = transform.rotation;
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }

    void Update()
    {
        if (player == null) return;

        cooldownTimer -= Time.deltaTime;

        float playerX = player.position.x;
        float playerY = player.position.y;
        float beeY = transform.position.y;

        if (!isAttacking && !isReturningToAir &&
            cooldownTimer <= 0f &&
            playerX >= attackRangeMin &&
            playerX <= attackRangeMax &&
            playerY < beeY)
        {
            float randomOffset = Random.Range(2f, 4f); 
            Vector2 directionToPlayer = ((Vector2)player.position - (Vector2)transform.position).normalized;
            Vector2 targetPosition = (Vector2)player.position + directionToPlayer * randomOffset;
            moveDirection = (targetPosition - (Vector2)transform.position).normalized;

            shouldMove = true;
            isAttacking = true;
            cooldownTimer = attackCooldown;

            float dir = player.position.x - transform.position.x;
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(dir) * Mathf.Abs(originalScaleX);
            transform.localScale = scale;

            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + 90f);
        }

        if (shouldMove)
        {
            Vector2 nextPos = (Vector2)transform.position + moveDirection * attackSpeed * Time.deltaTime;

            // Nếu vượt quá phạm vi tấn công thì dừng lại như chạm đất
            if (nextPos.x < attackRangeMin || nextPos.x > attackRangeMax)
            {
                shouldMove = false;
                StartCoroutine(ReturnToAir());
                return;
            }

            transform.position = new Vector3(nextPos.x, nextPos.y, transform.position.z);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            shouldMove = false;
            StartCoroutine(ReturnToAir());
        }
    }

    IEnumerator ReturnToAir()
    {
        isReturningToAir = true;

        yield return new WaitForSeconds(waitAfterHitGround);

        float targetY = transform.position.y + returnHeight;
        Vector2 target2D = new Vector2(transform.position.x, targetY);

        transform.localScale = new Vector3(originalScaleX, transform.localScale.y, transform.localScale.z);
        transform.rotation = originalRotation;

        while (transform.position.y < targetY)
        {
            Vector2 nextPos = Vector2.MoveTowards(transform.position, target2D, attackSpeed * Time.deltaTime);
            transform.position = new Vector3(nextPos.x, nextPos.y, transform.position.z);
            yield return null;
        }

        cooldownTimer = attackCooldown;
        isAttacking = false;
        isReturningToAir = false;
    }
}
