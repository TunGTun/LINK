using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharJump : LinkMonoBehaviour
{
    [Header("CharJump")]

    protected int _jumpCount;
    protected bool _canJump;
    protected bool _finishJump;

    public Vector2 jumpForce = new Vector2(5f, 10f); //Tạm

    [SerializeField] protected float _jumpForce = 6f;
    [SerializeField] protected int _maxExtraJump = 2;
    [SerializeField] protected float _fallSpeedLimit = -100f;

    [SerializeField] protected CharCtrl _charCtrl;

    public GameObject jumpFXPrefab;
    public Transform jumpFXPoint;

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
        this.JumpTransition();
    }

    protected virtual void Jump()
	{

        if (_charCtrl.CharState.GetIsDead()) return;

        if (!_canJump) return;

		if (_charCtrl.CharState.IsGrounded()) this.JumpOnGround();
		else this.JumpInAir();

        AudioManager.Instance.PlaySFX("Jump");
        this.JumpFX();

        _canJump = !_canJump;
        _jumpCount++;
    }

    protected virtual void JumpOnGround()
    {
        _charCtrl.Rigidbody2D.velocity = new Vector2(_charCtrl.Rigidbody2D.velocity.x,
            this._jumpForce + Mathf.Abs(_charCtrl.Rigidbody2D.velocity.x / 6));
    }

    protected virtual void JumpInAir()
    {
        _charCtrl.Rigidbody2D.velocity = new Vector2(_charCtrl.Rigidbody2D.velocity.x, this._jumpForce);
    }

	protected virtual void CheckJump()
    {
        if (_charCtrl.CharState.GetIsDead()) this._canJump = false;
        if (InputManager.Instance.StartJumpInput && _jumpCount < _maxExtraJump) this._canJump = true;
        if (InputManager.Instance.EndJumpInput) _finishJump = true;
        if (InputManager.Instance.StartJumpInput) _finishJump = false;
    }

    protected virtual void JumpHight()
    {
        if (_jumpCount > 1) return;
        if (!_finishJump) return;
        if (_charCtrl.Rigidbody2D.velocity.y > (this._jumpForce + Mathf.Abs(_charCtrl.Rigidbody2D.velocity.x / 4)) / 1.5
            || _charCtrl.Rigidbody2D.velocity.y < 0f) return;
        _charCtrl.Rigidbody2D.velocity = new Vector2(_charCtrl.Rigidbody2D.velocity.x, _charCtrl.Rigidbody2D.velocity.y / 3);
    }

    protected virtual void ResetJumpCount()
    {
        if (_charCtrl.Rigidbody2D.velocity.y > 0.1) return;
        if (_charCtrl.Rigidbody2D.velocity.y < -0.1) return;
        if (_charCtrl.CharState.IsGrounded())
            if (_jumpCount != 0) _jumpCount = 0;
    }

    protected virtual void FallSpeedLimit()
    {
        if (_charCtrl.Rigidbody2D.velocity.y > _fallSpeedLimit) return;
        _charCtrl.Rigidbody2D.velocity = new Vector2(_charCtrl.Rigidbody2D.velocity.x, _fallSpeedLimit);
    }

    protected virtual void JumpTransition()
    {
        if (_charCtrl.CharState.IsGrounded()) return;
        if (_charCtrl.CharState.GetIsDead()) return;
        if (_charCtrl.Rigidbody2D.velocity.y > 0)
        {
            _charCtrl.CharState.ChangeAnimationState("Jump");
            return;
        }
        if (_charCtrl.Rigidbody2D.velocity.y < 0)
        {
            _charCtrl.CharState.ChangeAnimationState("Fall");
            return;
        }
    }

    protected virtual void JumpFX()
    {
        GameObject jumpFX = Instantiate(jumpFXPrefab,jumpFXPoint.position, jumpFXPoint.rotation);
        Destroy(jumpFX, 0.5f);
    }
}
