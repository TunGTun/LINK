using System.Collections;
using UnityEngine;

public class CharMovement : LinkMonoBehaviour
{
    [Header("CharMovement")]
    [SerializeField] protected CharCtrl _charCtrl;
    [SerializeField] protected float _moveSpeed = 3f;
    protected float _xDirection;

    [SerializeField] private AudioSource moveAudioSource;
    [SerializeField] private AudioClip moveClip;
    [SerializeField] private float loopDelay = 0.2f;
    private Coroutine moveSFXCoroutine;

    //bool finishSFX = true;

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
    }

    protected virtual void GetXDirection()
    {
        //      if (_charCtrl.CharState.IsGrounded())
        //{
        //	_xDirection = InputManager.Instance.MoveAccelInput;
        //} 
        //      else
        //      {
        //          _xDirection = InputManager.Instance.MoveInput;
        //}

        _xDirection = InputManager.Instance.MoveAccelInput;

        if (_xDirection == 0) return;
        _charCtrl.CharState.SignMove = Mathf.Sign(InputManager.Instance.MoveAccelInput);
    }

    protected virtual void Move()
    {
        if (_charCtrl.CharState.GetIsDead())
        {
            _charCtrl.Rigidbody2D.velocity = new Vector2(0, _charCtrl.Rigidbody2D.velocity.y);
            return;
        }
        float _moveStep = _xDirection * _moveSpeed;
        //if (_charCtrl.CharState.WallJumping) return;
        if (_charCtrl.CharState.Dashing) return;
        _charCtrl.Rigidbody2D.velocity = new Vector2(_moveStep, _charCtrl.Rigidbody2D.velocity.y);

        // Phát âm thanh nếu đang di chuyển
        if (Mathf.Abs(_xDirection) > 0.1f && _charCtrl.CharState.IsGrounded())
        {
            PlayMoveSFX();
        }
        else
        {
            StopMoveSFX();
        }

        //if (finishSFX && Mathf.Abs(_xDirection) >= 0.9f) StartCoroutine(waitSFX());
        this.RunningFlip();
        this.RunningTransition();
    }

    //IEnumerator waitSFX()
    //{
    //    finishSFX = false;
    //    yield return new WaitForSeconds(0.5f);
    //    AudioManager.Instance.PlaySFX("Move");
    //    finishSFX = true;
    //}

    protected virtual void RunningFlip()
    {
        if (_xDirection < 0)
        {
            _charCtrl.SpriteRenderer.flipX = true;
            _charCtrl.BoxCollider2D.offset = new Vector2(0.03f, -0.065f);
        }
        if (_xDirection > 0)
        {
            _charCtrl.SpriteRenderer.flipX = false;
            _charCtrl.BoxCollider2D.offset = new Vector2(-0.03f, -0.065f);
        }
    }
    public float GetMoveSpeed()
    {
        return _moveSpeed;
    }

    public void SetMoveSpeed(float newSpeed)
    {
        _moveSpeed = newSpeed;
    }

    protected virtual void RunningTransition()
    {
        if (!_charCtrl.CharState.IsGrounded()) return;
        if (_xDirection > 0.1 || _xDirection < -0.1)
        {
            _charCtrl.CharState.ChangeAnimationState("Run");
        }
        else
        {
            _charCtrl.CharState.ChangeAnimationState("Idle");
        }
    }

    protected virtual void PlayMoveSFX()
    {
        if (moveSFXCoroutine == null)
        {
            moveSFXCoroutine = StartCoroutine(LoopMoveSFX());
        }
    }

    protected virtual void StopMoveSFX()
    {
        if (moveSFXCoroutine != null)
        {
            StopCoroutine(moveSFXCoroutine);
            moveSFXCoroutine = null;
        }

        if (moveAudioSource.isPlaying)
        {
            moveAudioSource.Stop();
        }
    }

    private IEnumerator LoopMoveSFX()
    {
        while (true)
        {
            moveAudioSource.PlayOneShot(moveClip);
            yield return new WaitForSeconds(moveClip.length + loopDelay);
        }
    }
}
