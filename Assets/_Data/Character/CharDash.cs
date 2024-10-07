using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharDash : LinkMonoBehaviour
{
	[Header("CharDash")]

	[SerializeField] protected bool _canDash;

	public float dashDuration = 0.2f; //Tạm
	public float dashForce = 30f; //Tạm

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
		this.CheckDash();
	}

	private void FixedUpdate()
	{
		this.Dash();
	}

	protected virtual void CheckDash() 
	{
		if (InputManager.Instance.DashInput) this._canDash = true;
	}

	protected virtual void Dash() 
	{
		if (!this._canDash) return;

		_charCtrl.CharState.Dashing = true;
		Invoke("StopDash", dashDuration);
		_charCtrl.Rigidbody2D.velocity = new Vector2(_charCtrl.CharState.SignMove * dashForce, _charCtrl.Rigidbody2D.velocity.y * dashForce / 8);

		this._canDash = false;
	}

	protected virtual void StopDash()
	{
		_charCtrl.Rigidbody2D.velocity = new Vector2(0, 0);
		_charCtrl.CharState.Dashing = false;
	}
}
