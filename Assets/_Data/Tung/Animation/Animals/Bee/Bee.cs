using UnityEngine;
using System.Collections;

public class Bee : MonoBehaviour
{
    public float flySpeed = 15f;
    public string groundLayerName = "Ground";
    public float initialDelayTime = 1f;
    public float groundCollisionDelayTime = 0.5f;
    public float flyHeight = 8f;

    private Rigidbody2D rb;
    private bool isFlying = false;
    private bool hasCollidedWithGround = false;
    private bool hasReachedFlyHeight = false;
    private Vector2 flyDirection;

    private Quaternion initialRotation;

    private GameObject target;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialRotation = transform.rotation; // Lưu lại góc quay ban đầu
        target = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(StartFlyingAfterInitialDelay());
    }

    IEnumerator StartFlyingAfterInitialDelay()
    {
        yield return new WaitForSeconds(initialDelayTime);
        if (target != null)
        {
            flyDirection = (target.transform.position - transform.position).normalized;
            isFlying = true;
            hasReachedFlyHeight = false;
            UpdateRotationTowardsTarget(target.transform.position); // Xoay Bee về phía player khi tấn công
        }
    }

    void FixedUpdate()
    {
        if (isFlying && !hasReachedFlyHeight)
        {
            rb.velocity = flyDirection * flySpeed;
        }

        if (hasReachedFlyHeight)
        {
            rb.velocity = Vector2.zero;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(groundLayerName) && !hasCollidedWithGround)
        {
            rb.velocity = Vector2.zero;
            isFlying = false;
            hasCollidedWithGround = true;
            StartCoroutine(RiseUpAfterGroundCollision());
        }
    }

    IEnumerator RiseUpAfterGroundCollision()
    {
        yield return new WaitForSeconds(groundCollisionDelayTime);
        ResetRotation();  // Reset lại rotation khi bay lên
        Vector2 riseDirection = new Vector2(0, flyHeight);
        rb.velocity = riseDirection;
        yield return new WaitForSeconds(flyHeight / flySpeed);
        hasReachedFlyHeight = true;
        hasCollidedWithGround = false;
        StartCoroutine(StartFlyingAfterInitialDelay()); // Tiếp tục kiểm tra và tấn công
    }

    // Hàm để xoay Bee về phía player khi tấn công
    void UpdateRotationTowardsTarget(Vector2 targetPosition)
    {
        Vector2 directionToTarget = targetPosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90f)); // Xoay Bee về phía player
    }

    // Hàm quay về vị trí ban đầu khi bay lên
    void ResetRotation()
    {
        transform.rotation = initialRotation; // Quay lại góc ban đầu
    }
}
