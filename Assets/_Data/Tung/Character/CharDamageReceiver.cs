using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharDamageReceiver : LinkMonoBehaviour
{
    [Header("CharDamageReceiver")]
    [SerializeField] protected Collider2D _Collider2D;
    [SerializeField] protected CharCtrl _CharCtrl;
    [SerializeField] protected float detectionCooldown = 2f;
    [SerializeField] protected float lastDetectionTime = 0f;

    public float bounceAmount = 1.0f;
    public CameraController cameraController;
    public Material originalMaterial, flashMaterial;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
        this.LoadCharCtrl();
        lastDetectionTime = Time.time - detectionCooldown;
        originalMaterial = _CharCtrl.SpriteRenderer.material;
    }

    private void Update()
    {
        this.CheckForEnemies();
    }

    protected virtual void LoadCollider()
    {
        if (this._Collider2D != null) return;
        this._Collider2D = GetComponentInParent<Collider2D>();
        Debug.LogWarning(transform.name + ": LoadCollider", gameObject);
    }

    protected virtual void LoadCharCtrl()
    {
        if (_CharCtrl != null) return;
        _CharCtrl = GetComponentInParent<CharCtrl>();
        Debug.LogWarning(transform.name + ": LoadCharCtrl", gameObject);
    }

    public void TakeDamage(int dmg, Vector2 enemyPosition, bool isSub = true)
    {
        if (!isSub)
        {
            this._CharCtrl.CharStats.AddHP(dmg);
            return;
        }
        this._CharCtrl.CharStats.SubHP(dmg);

        AudioManager.Instance.PlaySFX("TakeDamage");

        // Tính hướng va chạm (hướng từ enemy đến nhân vật)
        Vector2 hitDirection = (_CharCtrl.Rigidbody2D.position - enemyPosition).normalized;

        // Ép y = 1, giữ x như cũ
        hitDirection = new Vector2(hitDirection.x, 1).normalized;

        // Đẩy lùi theo hướng ngược lại
        Vector2 targetPos = _CharCtrl.Rigidbody2D.position + hitDirection * bounceAmount;
        _CharCtrl.Rigidbody2D.DOMove(targetPos, 0.1f);
        _CharCtrl.Rigidbody2D.velocity = _CharCtrl.Rigidbody2D.velocity / 2;

        _CharCtrl.SpriteRenderer.material = flashMaterial;
        StartCoroutine(MaterialCoroutine());
        this.cameraController.ShakeCamera();
    }

    IEnumerator MaterialCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        _CharCtrl.SpriteRenderer.material = originalMaterial;
    }

    void CheckForEnemies()
    {
        if (_CharCtrl.CharState.IsInvisible) return;
        if (Time.time - lastDetectionTime < detectionCooldown) return;

        Vector2 boxCenter = _Collider2D.bounds.center;
        Vector2 boxSize = _Collider2D.bounds.size;

        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(boxCenter, boxSize, 0f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemies"))
            {
                this.TakeDamage(1, hitCollider.transform.position);
                lastDetectionTime = Time.time;
            }
            if (hitCollider.CompareTag("DeadZone"))
            {
                this.TakeDamage(3, hitCollider.transform.position);
                lastDetectionTime = Time.time;
            }
            if (hitCollider.CompareTag("Bullet"))
            {
                this.TakeDamage(1, hitCollider.transform.position);
                lastDetectionTime = Time.time;
            }
        }
    }
}
