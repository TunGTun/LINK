using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharDash : LinkMonoBehaviour
{
	[Header("CharDash")]

	protected bool _canDash;

	public float dashDuration = 0.1f; //Tạm
	public float dashForce = 20f; //Tạm
    public float dashCooldown = 1f; // Thời gian chờ giữa các lần lướt

    private float _lastDashTime; // Lưu thời gian lần lướt cuối cùng

	//public int dashMP = 1;

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
		if (InputManager.Instance.DashInput && Time.time >= _lastDashTime + dashCooldown
			//&& _charCtrl.CharStats.currMP >= this.dashMP
			) this._canDash = true;
	}

	protected virtual void Dash() 
	{
		if (!this._canDash) return;

        if (_charCtrl.CharState.GetIsDead()) return;

        _charCtrl.CharState.Dashing = true;
		Invoke("StopDash", dashDuration);
		_charCtrl.Rigidbody2D.velocity = new Vector2(_charCtrl.CharState.SignMove * dashForce, _charCtrl.Rigidbody2D.velocity.y * dashForce / 8);

		this._canDash = false;
        _lastDashTime = Time.time;
		//_charCtrl.CharStats.SubMP(1);
    }

	protected virtual void StopDash()
	{
		_charCtrl.Rigidbody2D.velocity = new Vector2(0, 0);
		_charCtrl.CharState.Dashing = false;
	}
}
