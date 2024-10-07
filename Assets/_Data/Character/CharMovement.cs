using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : LinkMonoBehaviour
{
    [Header("CharMovement")]
    [SerializeField] protected CharCtrl _charCtrl;
    [SerializeField] protected float _moveSpeed = 10.0f;
    [SerializeField] protected float _xDirection;

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
        this.GetXDirection();
    }

    private void FixedUpdate()
    {
        this.Move();
        //this.WallSlide();
    }

    protected virtual void GetXDirection()
    {
        _xDirection = InputManager.Instance.MoveInput;
        _charCtrl.CharState.SignMove = Mathf.Sign(_xDirection);
    }

    protected virtual void Move()
    {
        float _moveStep = _xDirection * _moveSpeed;
        //if (!_charCtrl.CharState.IsWallCling())
        if (_charCtrl.CharState.WallJumping) return;
        if (_charCtrl.CharState.Dashing) return;

        _charCtrl.Rigidbody2D.velocity = new Vector2(_moveStep, _charCtrl.Rigidbody2D.velocity.y);
        //else _charCtrl.Rigidbody2D.velocity = new Vector2(_moveStep, -4f);
	}

 //   protected virtual void WallSlide()
 //   {
 //       if (!_charCtrl.CharState.IsWallCling()) return;
	//	_charCtrl.Rigidbody2D.velocity = new Vector2(_charCtrl.Rigidbody2D.velocity.y, Mathf.Clamp(_charCtrl.Rigidbody2D.velocity.y, -4f, float.MaxValue));
	//}
}
