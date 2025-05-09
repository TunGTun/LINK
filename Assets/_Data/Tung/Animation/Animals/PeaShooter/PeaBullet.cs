using System.Collections;
using UnityEngine;

public class PeaBullet : LinkMonoBehaviour
{
    public float speed = 5f;
    public float fallGravityScale = 5f;
    public float destroyDelay = 1f;
    public string wallLayerName = "Wall";
    public Sprite bulletNormal;
    public Sprite bulletPiece;

    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer spriteRenderer;
    private bool isFalling = false;
    private int wallLayer;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        wallLayer = LayerMask.NameToLayer(wallLayerName);
    }

    void OnEnable()
    {
        // Reset trạng thái đạn mỗi khi được bật lại
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();

        isFalling = false;
        spriteRenderer.sprite = bulletNormal;
        rb.gravityScale = 0;
        rb.velocity = Vector2.right * speed; // Bay sang phải khi được bật lại
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isFalling && (other.CompareTag("Player") || other.gameObject.layer == wallLayer))
        {
            StartFalling();
        }
    }

    void StartFalling()
    {
        isFalling = true;
        spriteRenderer.sprite = bulletPiece;
        rb.velocity = Vector2.zero;
        rb.gravityScale = fallGravityScale;

        StartCoroutine(UnActive());
    }

    IEnumerator UnActive()
    {
        yield return new WaitForSeconds(destroyDelay);
        gameObject.SetActive(false);
    }
}
