using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageSender : LinkMonoBehaviour
{
    public virtual void Send(Transform obj, int dmg)
    {
        DamageReceiver damageReceiver = obj.GetComponentInChildren<DamageReceiver>();
        if (damageReceiver == null) return;
        this.Send(damageReceiver, dmg);
    }

    public virtual void Send(DamageReceiver damageReceiver, int dmg)
    {
        damageReceiver.TakeDamage(dmg);
        this.OnImpact();
    }

    protected abstract void OnImpact();

    //protected virtual void CreateImpactFX()
    //{
    //    string fxName = this.GetImpactFX();

    //    Vector3 hitPos = transform.position;
    //    Quaternion hitRot = transform.rotation;
    //    Transform fxImpact = FXSpawner.Instance.Spawn(fxName, hitPos, hitRot);
    //    fxImpact.gameObject.SetActive(true);
    //}

    //protected virtual string GetImpactFX()
    //{
    //    return FXSpawner.impactOne;
    //}
}
