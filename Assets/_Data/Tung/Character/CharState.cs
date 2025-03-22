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
    private string _currentAnimState;

    //public bool WallJumping { get; set;}
    public bool Dashing { get; set; }
    public bool IsInvisible{ get; set; }
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
        
    }

    public virtual bool IsGrounded()
    {
        Vector2 boxSize = new Vector2(_charCtrl.BoxCollider2D.bounds.size.x - 0.02f, 0.05f);  // Giảm chiều rộng để tránh cạnh bên
        Vector2 boxCenter = new Vector2(_charCtrl.BoxCollider2D.bounds.center.x, _charCtrl.BoxCollider2D.bounds.min.y - 0.04f);

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

    public virtual void ChangeAnimationState(string newState)
    {
        // Stop the same animation from interrupting itself
        if (_currentAnimState == newState) return;

        // Play the animation
        _charCtrl.Animator.Play(newState);

        // Reassign the current state
        _currentAnimState = newState;
    }

    public virtual void SetIsDead(bool isDead)
    {
        this.isDead = isDead;
    }

    public virtual bool GetIsDead()
    {
        return this.isDead;
    }
}
