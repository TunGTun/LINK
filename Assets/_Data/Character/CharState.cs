using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class CharState : LinkMonoBehaviour
{
	[Header("CharState")]
	[SerializeField] protected bool _isGrounded;
    [SerializeField] protected bool isDead = false;
    //[SerializeField] protected bool _isTouchingCeiling;
    //[SerializeField] protected bool _isTouchingLeftWall;
    //[SerializeField] protected bool _isTouchingRightWall;
    //[SerializeField] protected bool _isWallCling;
    //[SerializeField] protected bool _isUnderside;
    [SerializeField] private string _currentAnimState;

    //public bool WallJumping { get; set;}
    public bool Dashing { get; set; }
    public float SignMove { get; set;} //Tạm

    [SerializeField] protected CharCtrl _charCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCharCtrl();
		this.SignMove = 1;
    }

    protected virtual void LoadCharCtrl()
    {
        if (_charCtrl != null) return;
        _charCtrl = GetComponentInParent<CharCtrl>();
        Debug.LogWarning(transform.name + ": LoadCharCtrl", gameObject);
    }

    private void Update()
    {
        //this.IsThroughTerrain();
    }

    public virtual bool IsGrounded()
    {
        Vector2 boxSize = new Vector2(_charCtrl.BoxCollider2D.bounds.size.x - 0.1f, 0.05f);  // Giảm chiều rộng để tránh cạnh bên
        Vector2 boxCenter = new Vector2(_charCtrl.BoxCollider2D.bounds.center.x, _charCtrl.BoxCollider2D.bounds.min.y - 0.04f);

        // Vẽ hộp kiểm tra va chạm trong Scene View
        //Debug.DrawLine(boxCenter + new Vector2(-boxSize.x / 2, boxSize.y / 2), boxCenter + new Vector2(boxSize.x / 2, boxSize.y / 2), Color.green);
        //Debug.DrawLine(boxCenter + new Vector2(boxSize.x / 2, boxSize.y / 2), boxCenter + new Vector2(boxSize.x / 2, -boxSize.y / 2), Color.green);
        //Debug.DrawLine(boxCenter + new Vector2(boxSize.x / 2, -boxSize.y / 2), boxCenter + new Vector2(-boxSize.x / 2, -boxSize.y / 2), Color.green);
        //Debug.DrawLine(boxCenter + new Vector2(-boxSize.x / 2, -boxSize.y / 2), boxCenter + new Vector2(-boxSize.x / 2, boxSize.y / 2), Color.green);

        Collider2D hit = Physics2D.OverlapBox(boxCenter, boxSize, 0f, LayerMask.GetMask("Ground"));

        if (hit != null)
        {
            // Chỉ true khi góc tiếp xúc gần bằng 90 độ (mặt trên)
            float angle = Vector2.Angle(hit.transform.up, Vector2.up);
            this._isGrounded = angle < 5f;  // Dung sai góc nhỏ
        }
        else
        {
            this._isGrounded = false;
        }

        return _isGrounded;
    }

    //public virtual void CheckCollisions()
    //{

        //Vector2 boxSize = _charCtrl.BoxCollider2D.bounds.size;
        //Vector2 boxCenter = _charCtrl.BoxCollider2D.bounds.center;

        //float checkDistance = 0.1f; // Khoảng cách kiểm tra va chạm

        //// Kiểm tra mặt đất (dưới)
        //_isGrounded = Physics2D.OverlapBox(
        //    boxCenter + Vector2.down * (boxSize.y / 2 + checkDistance),
        //    new Vector2(boxSize.x * 0.9f, checkDistance),
        //    0f,
        //    LayerMask.GetMask("Platform")
        //) != null;

        //// Kiểm tra trần nhà (trên)
        //_isTouchingCeiling = Physics2D.OverlapBox(
        //    boxCenter + Vector2.up * (boxSize.y / 2 + checkDistance),
        //    new Vector2(boxSize.x * 0.9f, checkDistance),
        //    0f,
        //    LayerMask.GetMask("Platform")
        //) != null;

        //// Kiểm tra tường bên trái
        //_isTouchingLeftWall = Physics2D.OverlapBox(
        //    boxCenter + Vector2.left * (boxSize.x / 2 + checkDistance),
        //    new Vector2(checkDistance, boxSize.y * 0.9f),
        //    0f,
        //    LayerMask.GetMask("Platform")
        //) != null;

        //// Kiểm tra tường bên phải
        //_isTouchingRightWall = Physics2D.OverlapBox(
        //    boxCenter + Vector2.right * (boxSize.x / 2 + checkDistance),
        //    new Vector2(checkDistance, boxSize.y * 0.9f),
        //    0f,
        //    LayerMask.GetMask("Platform")
        //) != null;

    //}

    //public virtual void SetGrounded(bool isGround)
    //{
    //    this._isGrounded = isGround;
    //}

    //public virtual void SetUnderside(bool isUnderside)
    //{
    //	this._isUnderside = isUnderside;
    //}

    //protected virtual void IsThroughTerrain()
    //{
    //    if (this._isTouchingCeiling || this._isTouchingRightWall || this._isTouchingLeftWall)
    //    {
    //        _charCtrl.BoxCollider2D.isTrigger = true;
    //    }
    //    if (!this._isTouchingCeiling && !this._isTouchingRightWall 
    //        && !this._isTouchingLeftWall && !this._isGrounded)
    //    {
    //        _charCtrl.BoxCollider2D.isTrigger = false;
    //    }
    //}

    public virtual void ChangeAnimationState(string newState)
    {
        // Stop the same animation from interrupting itself
        if (_currentAnimState == newState) return;

        // Play the animation
        _charCtrl.Animator.Play(newState);

        // Reassign the current state
        _currentAnimState = newState;
    }

    //   private void OnDrawGizmos()
    //{
    //	// Chỉ hiển thị khi BoxCollider2D đã được gắn
    //	if (_charCtrl != null && _charCtrl.BoxCollider2D != null)
    //	{
    //		// Lấy thông tin về kích thước và vị trí hộp từ hàm IsGrounded
    //		Vector2 boxSize = new Vector2(_charCtrl.BoxCollider2D.bounds.size.x, _charCtrl.BoxCollider2D.bounds.size.y);  // chiều cao rất nhỏ để kiểm tra bề mặt dưới
    //		Vector2 boxCenter = new Vector2(_charCtrl.BoxCollider2D.bounds.center.x, _charCtrl.BoxCollider2D.bounds.center.y);  // Tọa độ ngay dưới nhân vật

    //		// Chọn màu để dễ dàng nhìn thấy hộp trong Gizmos
    //		Gizmos.color = Color.red;

    //		// Vẽ hộp dùng để kiểm tra mặt đất
    //		Gizmos.DrawWireCube(boxCenter, boxSize);
    //	}
    //}

    public virtual bool IsDead()
    {
        return this._charCtrl.CharStats.currHP <= 0;
    }

    public virtual void SetIsDead(bool isDead)
    {
        this.isDead = isDead;
    }
}
