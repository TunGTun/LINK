using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCtrl : LinkMonoBehaviour
{
    [Header("CharCtrl")]
    [SerializeField] protected Rigidbody2D _Rigidbody2D;
    public Rigidbody2D Rigidbody2D => _Rigidbody2D;

    [SerializeField] protected BoxCollider2D _BoxCollider2D;
    public BoxCollider2D BoxCollider2D => _BoxCollider2D;

    [SerializeField] protected CharState _CharState;
    public CharState CharState => _CharState;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody();
        this.LoadCollider();
        this.LoadCharState();
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
        Debug.LogWarning(transform.name + ": LoadCollider", gameObject);
    }

    protected virtual void LoadCharState()
    {
        if (_CharState != null) return;
        _CharState = GetComponentInChildren<CharState>();
        Debug.LogWarning(transform.name + ": LoadCharState", gameObject);
    }
}
