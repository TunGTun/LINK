using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCtrl : LinkMonoBehaviour
{
    [Header("ParticleCtrl")]

    [SerializeField] protected CharCtrl _charCtrl;
    [SerializeField] protected ParticleSystem[] particles;

    [SerializeField] protected int occurAfterVelocity = 4; // toc do can dat
    [SerializeField] protected float dustFormationPeriod = 0.05f;
    [SerializeField] protected float counter;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCharCtrl();
        this.LoadParticle();
    }

    protected virtual void LoadCharCtrl()
    {
        if (_charCtrl != null) return;
        _charCtrl = GetComponentInParent<CharCtrl>();
        Debug.LogWarning(transform.name + ": LoadCharCtrl", gameObject);
    }

    protected virtual void LoadParticle()
    {
        if (particles.Length > 0) return;
        particles = GetComponentsInChildren<ParticleSystem>();
        Debug.LogWarning(transform.name + ": LoadParticle", gameObject);
    }

    private void Update()
    {
        this.MovementParticle();
    }

    protected virtual void MovementParticle()
    {
        counter += Time.deltaTime;

        if (_charCtrl.CharState.IsGrounded() && Mathf.Abs(_charCtrl.Rigidbody2D.velocity.x) > occurAfterVelocity)
        {
            if (counter > dustFormationPeriod)
            {
                particles[0].Play();
                counter = 0;
            }
        }
    }
}
