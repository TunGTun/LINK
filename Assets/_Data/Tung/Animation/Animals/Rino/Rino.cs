using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rino : LinkMonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public float detectionRangeX = 5f;
    private float sizeY;
    private float sizeX;
    private Vector2 moveDirection;
    public bool canMove = false;
    public float checkInterval = 2f;
    private float checkTimer;
    public bool isFacingRight = true;

    private Animator animator;

    protected override void Start()
    {
        base.Start();
        Collider2D collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        sizeY = collider.bounds.size.y;
        sizeX = collider.bounds.extents.x;
        checkTimer = checkInterval;
    }

    void Update()
    {
        if (player == null) return;

        if (!canMove)
        {
            moveDirection = Vector2.zero;
            checkTimer -= Time.deltaTime;
            if (checkTimer <= 0)
            {
                if (HasPlayerChangedSide())
                {
                    CheckPlayerPosition();
                }
            }
        }

        if (canMove)
        {
            transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
            AdjustRotation(moveDirection.x);
        }
    }

    private bool HasPlayerChangedSide()
    {
        return (player.position.x > transform.position.x && !isFacingRight) ||
               (player.position.x < transform.position.x && isFacingRight);
    }

    private void CheckPlayerPosition()
    {
        Vector2 enemyPos = transform.position;
        Vector2 playerPos = player.position;

        float distanceX = Mathf.Abs(playerPos.x - enemyPos.x);
        float distanceY = Mathf.Abs(playerPos.y - enemyPos.y);

        if (distanceX < detectionRangeX && distanceX > 1f && distanceY < sizeY / 2)
        {
            moveDirection = new Vector2(playerPos.x - enemyPos.x, 0).normalized;
            canMove = true;
            animator.SetBool("canMove", canMove);
            checkTimer = checkInterval;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            canMove = false;
            isFacingRight = !isFacingRight;
            animator.SetBool("canMove", canMove);
            transform.position -= (Vector3)moveDirection * 0.3f;
            AdjustRotation(-moveDirection.x);
        }
    }

    private void AdjustRotation(float sign)
    {
        if (sign > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (sign < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
