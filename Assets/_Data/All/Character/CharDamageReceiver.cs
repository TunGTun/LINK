using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharDamageReceiver : DamageReceiver
{
    [Header("CharDamageReceiver")]
    [SerializeField] protected Collider2D _Collider2D;
    [SerializeField] protected CharCtrl _CharCtrl;
    [SerializeField] protected float detectionCooldown = 2f;
    [SerializeField] protected float lastDetectionTime = 0f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
        this.LoadCharCtrl();
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

    public override void TakeDamage(int dmg, bool isSub = true)
    {
        if (this._CharCtrl.CharState.IsDead()) return;
        if (!isSub)
        {
            this._CharCtrl.CharStats.AddHP(dmg);
            return;
        }
        this._CharCtrl.CharStats.SubHP(dmg);
        this.CheckIsDead();
    }

    protected override void CheckIsDead()
    {
        if (!this._CharCtrl.CharState.IsDead()) return;
        this._CharCtrl.CharState.SetIsDead(true);
        this.OnDead();
    }

    protected override void OnDead()
    {
        _CharCtrl.CharState.ChangeAnimationState("Die");
    }

    void CheckForEnemies()
    {
        //Debug.Log(Time.time - lastDetectionTime);
        if (Time.time - lastDetectionTime < detectionCooldown) return;

        Vector2 boxCenter = _Collider2D.bounds.center;
        Vector2 boxSize = _Collider2D.bounds.size;

        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(boxCenter, boxSize, 0f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemies"))
            {
                this.TakeDamage(1);
                lastDetectionTime = Time.time;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_Collider2D.bounds.center, _Collider2D.bounds.size);
    }
}
