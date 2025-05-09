using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class CharCtrl : LinkMonoBehaviour
{
    [Header("CharCtrl")]
    [SerializeField] protected Rigidbody2D _Rigidbody2D;
    public Rigidbody2D Rigidbody2D => _Rigidbody2D;

    [SerializeField] protected BoxCollider2D _BoxCollider2D;
    public BoxCollider2D BoxCollider2D => _BoxCollider2D;

    [SerializeField] protected CharState _CharState;
    public CharState CharState => _CharState;

    [SerializeField] protected SpriteRenderer _SpriteRenderer;
    public SpriteRenderer SpriteRenderer => _SpriteRenderer;

    [SerializeField] protected Animator _Animator;
    public Animator Animator => _Animator;

    [SerializeField] protected CharStats _CharStats;
    public CharStats CharStats => _CharStats;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody();
        this.LoadCollider();
        this.LoadCharState();
        this.LoadSpriteRenderer();
        this.LoadAnimator();
        this.LoadCharStats();
    }

    protected virtual void LoadRigidbody()
    {
        if (_Rigidbody2D != null) return;
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        _Rigidbody2D.freezeRotation = true;
        _Rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        _Rigidbody2D.sleepMode = RigidbodySleepMode2D.NeverSleep;
        _Rigidbody2D.interpolation = RigidbodyInterpolation2D.Interpolate;
        Debug.LogWarning(transform.name + ": LoadRigidbody", gameObject);
    }

    protected virtual void LoadCollider()
    {
        if (this._BoxCollider2D != null) return;
        this._BoxCollider2D = GetComponent<BoxCollider2D>();
        this._BoxCollider2D.size = new Vector2(0.5f, 0.88f);
        this._BoxCollider2D.offset = new Vector2(-0.03f, -0.065f);
        Debug.LogWarning(transform.name + ": LoadCollider", gameObject);
    }

    protected virtual void LoadCharState()
    {
        if (_CharState != null) return;
        _CharState = GetComponentInChildren<CharState>();
        Debug.LogWarning(transform.name + ": LoadCharState", gameObject);
    }

    protected virtual void LoadSpriteRenderer()
    {
        if (_SpriteRenderer != null) return;
        _SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Debug.LogWarning(transform.name + ": LoadSpriteRenderer", gameObject);
    }

    protected virtual void LoadAnimator()
    {
        if (_Animator != null) return;
        _Animator = GetComponentInChildren<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }

    protected virtual void LoadCharStats()
    {
        if (_CharStats != null) return;
        _CharStats = GetComponentInChildren<CharStats>();
        Debug.LogWarning(transform.name + ": LoadCharStats", gameObject);
    }
}
