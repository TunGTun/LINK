using System.Collections;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    public Transform player;
    public float A = 5.0f;
    public float moveSpeed = 2.0f;

    private Animator animator;
    private bool isDead = false;
    private bool isMoving = false;
    //private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return; // Nếu chết rồi thì không làm gì nữa

        if (player.position.x > A)
        {
            isMoving = true;
            //isAttacking = true;
            animator.SetBool("isAttacking", true);


        }
        if (isMoving)
        {
            // Di chuyển thẳng sang phải
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;

            // Lật hướng nhìn phải (nếu cần)
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isDead && other.CompareTag("Enemies"))
        {
            isDead = true;
            animator.SetBool("attacktoDeath",true);
            animator.SetBool("isAttacking", false); // tắt animation tấn công
        }
    }

    // Gọi từ Animation Event trong animation "Death"
    public void OnDeathAnimationEnd()
    {
        Destroy(gameObject);
    }
}
