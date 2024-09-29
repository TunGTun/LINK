using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharState : LinkMonoBehaviour
{
    [Header("CharState")]
    [SerializeField] protected bool _isGrounded;
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
        this.IsGrounded();
    }

    public virtual bool IsGrounded()
    {
        this._isGrounded = _charCtrl.BoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"));
        return this._isGrounded;
    }

    public virtual void SetGrounded(bool isGround)
    {
        this._isGrounded = isGround;
    }
}
