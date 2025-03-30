using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharFlip : LinkMonoBehaviour
{
    [SerializeField] protected CharCtrl _charCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCharCtrl();
    }

    protected virtual void LoadCharCtrl()
    {
        if (_charCtrl != null) return;
        _charCtrl = GetComponentInParent<CharCtrl>();
        Debug.LogWarning(transform.name + ": LoadCharCtrl", gameObject);
    }

    private void Update()
    {
        this.FlipFlowChar();
    }

    public virtual void FlipFlowChar()
    {
        if (_charCtrl.CharState.SignMove < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (_charCtrl.CharState.SignMove > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
