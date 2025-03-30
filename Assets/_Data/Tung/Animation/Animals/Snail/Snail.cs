using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : LinkMonoBehaviour
{
    public float leftX;
    public float rightX;
    public float speed = 3f;

    private bool movingRight = false;
    private bool isStopped = false;
    private int stopCounter = 0;
    private BoxCollider2D snailCollider;
    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;
    private Rigidbody2D playerRb;
    private Animator animator;
    public BoxCollider2D dmgCollider;

    protected override void Start()
    {
        base.Start();
        snailCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        if (snailCollider != null)
        {
            originalColliderSize = snailCollider.size;
            originalColliderOffset = snailCollider.offset;
        }
    }

    void Update()
    {
        if (isStopped) return;

        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (transform.position.x >= rightX)
            {
                movingRight = false;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (transform.position.x <= leftX)
            {
                movingRight = true;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        stopCounter++;
        HandlePlayerCollision(collision.gameObject, collision.collider);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        HandlePlayerCollision(collider.gameObject, collider);
    }

    private void HandlePlayerCollision(GameObject obj, Collider2D objCollider)
    {
        if (obj.CompareTag("Player"))
        {
            BoxCollider2D playerCollider = obj.GetComponent<BoxCollider2D>();
            playerRb = obj.GetComponent<Rigidbody2D>();
            if (playerCollider == null || playerRb == null) return;

            float playerBottom = playerCollider.bounds.min.y;
            float snailTop = snailCollider.bounds.max.y - (snailCollider.size.y * 0.1f);

            if (playerBottom < snailTop)
            {
                stopCounter--;
            }
            else
            {
                if (isStopped)
                {
                    if (stopCounter > 1)
                    {
                        animator.SetBool("isMoving", isStopped);
                        snailCollider.size = originalColliderSize;
                        snailCollider.offset = originalColliderOffset;
                        snailCollider.isTrigger = isStopped;
                        dmgCollider.enabled = isStopped;
                        isStopped = !isStopped;
                        playerRb.velocity = new Vector2(playerRb.velocity.x, 6f);
                        movingRight = !movingRight;
                        transform.rotation = Quaternion.Euler(0, transform.rotation.y == 0 ? 180 : 0, 0);
                    }
                }
                else
                {
                    animator.SetBool("isMoving", isStopped);
                    snailCollider.size = new Vector2(0.45f, 0.54f);
                    snailCollider.offset = new Vector2(0.07f, -0.11f);
                    snailCollider.isTrigger = isStopped;
                    dmgCollider.enabled = isStopped;
                    stopCounter = 0;
                    isStopped = !isStopped;
                    playerRb.velocity = new Vector2(playerRb.velocity.x, 5f);
                }

            }
        }
    }
}