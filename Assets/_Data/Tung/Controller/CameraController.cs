using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : LinkMonoBehaviour
{
    [Header("CameraControllern")]
    [SerializeField] protected Transform target;
    protected Vector2 velocity = Vector2.zero;

    [Range(0f, 1f)]
    [SerializeField] protected float smoothTime;

    [SerializeField] protected Vector2 positionOffset;

    [SerializeField] protected Vector2 xAxisLimit;
    [SerializeField] protected Vector2 yAxisLimit;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTarget();
    }

    protected virtual void LoadTarget()
    {
        if (this.target != null) return;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.LogWarning(transform.name + ": LoadTarget", gameObject);
    }

    private void LateUpdate()
    {
        Vector2 targetPosition = (Vector2)target.position + positionOffset;
        targetPosition = new Vector2(Mathf.Clamp(targetPosition.x, xAxisLimit.x, xAxisLimit.y), Mathf.Clamp(targetPosition.y, yAxisLimit.x, yAxisLimit.y));
        transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

}
