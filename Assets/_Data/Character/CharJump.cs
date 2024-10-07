using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.CollabMigration;
using UnityEngine;
using UnityEngine.UIElements;

public class CharJump : LinkMonoBehaviour
{
    [Header("CharJump")]

    protected int _jumpCount;
    protected bool _canJump;
    protected bool _finishJump;

    public float wallJumpDuration = 0.5f; //Tạm
    public Vector2 jumpForce = new Vector2(5f, 10f); //Tạm

    [SerializeField] protected float _jumpForce = 6f;
    [SerializeField] protected int _maxExtraJump = 2;
    [SerializeField] protected float _fallSpeedLimit = -5f;

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
        this.CheckJump();
        this.ResetJumpCount();
    }

    private void FixedUpdate()
	{
        this.FallSpeedLimit();
        this.JumpHight();
		this.Jump();
    }

    protected virtual void Jump()
	{
		if (!_canJump) return;

		if (_charCtrl.CharState.IsGrounded()) this.JumpOnGround();
        else if (_charCtrl.CharState.IsWallCling()) this.JumpOnWall();
		else this.JumpInAir();

        _canJump = !_canJump;
        _jumpCount++;
    }

    protected virtual void JumpOnGround()
    {
        _charCtrl.Rigidbody2D.velocity = new Vector2(_charCtrl.Rigidbody2D.velocity.x,
            this._jumpForce + Mathf.Abs(_charCtrl.Rigidbody2D.velocity.x / 4));
    }

    protected virtual void JumpInAir()
    {
        _charCtrl.Rigidbody2D.velocity = new Vector2(_charCtrl.Rigidbody2D.velocity.x, this._jumpForce);
    }

    protected virtual void JumpOnWall() //Tạm
    {
        _charCtrl.CharState.WallJumping = true;
        Invoke("StopWallJump", wallJumpDuration);
        _charCtrl.Rigidbody2D.velocity = new Vector2( -_charCtrl.CharState.SignMove * jumpForce.x, jumpForce.y);
	}

    protected virtual void StopWallJump()
    {
		_charCtrl.CharState.WallJumping = false;
	}

	protected virtual void CheckJump()
    {
        if (InputManager.Instance.StarJumpInput && _jumpCount < _maxExtraJump) this._canJump = true;
        if (InputManager.Instance.EndJumpInput) _finishJump = true;
    }

    protected virtual void JumpHight()
    {
        if (_jumpCount > 1) return;
        if (!_finishJump) return;
        if (_charCtrl.Rigidbody2D.velocity.y > (this._jumpForce + Mathf.Abs(_charCtrl.Rigidbody2D.velocity.x / 4)) / 1.5
            || _charCtrl.Rigidbody2D.velocity.y < 0f) return;
        _charCtrl.Rigidbody2D.velocity = new Vector2(_charCtrl.Rigidbody2D.velocity.x, _charCtrl.Rigidbody2D.velocity.y / 3);
        _finishJump = false;
    }

    protected virtual void ResetJumpCount()
    {
        if (_charCtrl.BoxCollider2D.isTrigger) return;
        if (_charCtrl.CharState.IsGrounded() || _charCtrl.CharState.IsWallCling())
            if (_jumpCount != 0) _jumpCount = 0;
    }

    protected virtual void FallSpeedLimit()
    {
        if (_charCtrl.Rigidbody2D.velocity.y > _fallSpeedLimit) return;
        _charCtrl.Rigidbody2D.velocity = new Vector2(_charCtrl.Rigidbody2D.velocity.x, _fallSpeedLimit);
    }
}
