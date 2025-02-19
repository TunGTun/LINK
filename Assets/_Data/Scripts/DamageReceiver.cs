using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageReceiver : LinkMonoBehaviour
{

    //protected override void LoadComponents()
    //{
    //    base.LoadComponents();
    //    this.LoadCollider();
    //}

    //protected virtual void LoadCollider()
    //{
    //    if (this.m_collider2D != null) return;
    //    this.m_collider2D = GetComponent<Collider2D>();
    //    this.m_collider2D.isTrigger = true;
    //    //this.sphereCollider.radius = 0.25f;
    //    Debug.LogWarning(transform.name + ": LoadCollider", gameObject);
    //}

    public abstract void TakeDamage(int dmg, bool isSub = true);

    protected abstract void CheckIsDead();

    protected abstract void OnDead();
}
