using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class CharState : LinkMonoBehaviour
{
	[Header("CharState")]
	[SerializeField] protected bool _isGrounded;
	[SerializeField] protected bool _isWallCling;
	[SerializeField] protected bool _isUnderside;

	public bool WallJumping { get; set;}
	public bool Dashing { get; set;}
	public float SignMove { get; set;} //Tạm

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
        this.IsWallCling();
        this.IsUnderside();
		this.IsThroughTerrain();
    }

    public virtual bool IsGrounded()
    {
        Vector2 boxSize = new Vector2(_charCtrl.BoxCollider2D.bounds.size.x, _charCtrl.BoxCollider2D.bounds.size.y);  // chiều cao rất nhỏ để kiểm tra bề mặt dưới
        Vector2 boxCenter = new Vector2(_charCtrl.BoxCollider2D.bounds.center.x, _charCtrl.BoxCollider2D.bounds.center.y);  // Tọa độ ngay dưới nhân vật

        // Kiểm tra xem hộp có chạm vào lớp "Ground" không
        this._isGrounded = Physics2D.OverlapBox(boxCenter, boxSize, 0f, LayerMask.GetMask("Ground")) != null;

        return this._isGrounded;
    }

    //public virtual void SetGrounded(bool isGround)
    //{
    //    this._isGrounded = isGround;
    //}

	public virtual bool IsWallCling() //cần xem lại
	{
        if (this._isGrounded || this._isUnderside) 
        {
			this._isWallCling = false;
            return this._isWallCling;
		}

		if (InputManager.Instance.MoveInput == 0)
		{
			this._isWallCling = false;
			return this._isWallCling;
		}

		Vector2 boxSize = new Vector2(_charCtrl.BoxCollider2D.bounds.size.x, _charCtrl.BoxCollider2D.bounds.size.y);  // chiều cao rất nhỏ để kiểm tra bề mặt dưới
		Vector2 boxCenter = new Vector2(_charCtrl.BoxCollider2D.bounds.center.x, _charCtrl.BoxCollider2D.bounds.center.y);  // Tọa độ ngay dưới nhân vật

		this._isWallCling = Physics2D.OverlapBox(boxCenter, boxSize, 0f, LayerMask.GetMask("Wall")) != null;

		return this._isWallCling;
	}

	public virtual bool IsUnderside()
	{
		Vector2 boxSize = new Vector2(_charCtrl.BoxCollider2D.bounds.size.x, _charCtrl.BoxCollider2D.bounds.size.y);  // chiều cao rất nhỏ để kiểm tra bề mặt dưới
		Vector2 boxCenter = new Vector2(_charCtrl.BoxCollider2D.bounds.center.x, _charCtrl.BoxCollider2D.bounds.center.y);  // Tọa độ ngay dưới nhân vật

		this._isUnderside = Physics2D.OverlapBox(boxCenter, boxSize, 0f, LayerMask.GetMask("Underside")) != null;

		return this._isUnderside;
	}

	//public virtual void SetUnderside(bool isUnderside)
	//{
	//	this._isUnderside = isUnderside;
	//}

	protected virtual void IsThroughTerrain()
	{
		if (this._isUnderside)
		{
			_charCtrl.BoxCollider2D.isTrigger = true;
		}
		if (!this._isUnderside && !this._isGrounded && !this._isWallCling)
		{
			_charCtrl.BoxCollider2D.isTrigger = false;
		}
	}



	private void OnDrawGizmos()
	{
		// Chỉ hiển thị khi BoxCollider2D đã được gắn
		if (_charCtrl != null && _charCtrl.BoxCollider2D != null)
		{
			// Lấy thông tin về kích thước và vị trí hộp từ hàm IsGrounded
			Vector2 boxSize = new Vector2(_charCtrl.BoxCollider2D.bounds.size.x, _charCtrl.BoxCollider2D.bounds.size.y);  // chiều cao rất nhỏ để kiểm tra bề mặt dưới
			Vector2 boxCenter = new Vector2(_charCtrl.BoxCollider2D.bounds.center.x, _charCtrl.BoxCollider2D.bounds.center.y);  // Tọa độ ngay dưới nhân vật

			// Chọn màu để dễ dàng nhìn thấy hộp trong Gizmos
			Gizmos.color = Color.red;

			// Vẽ hộp dùng để kiểm tra mặt đất
			Gizmos.DrawWireCube(boxCenter, boxSize);
		}
	}
}
