using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndReset : LinkMonoBehaviour
{
    //[SerializeField] Transform targetTransform;
    [SerializeField] float duration;
    [SerializeField] LeanTweenType loopType;

    protected override void Start()
    {
        base.Start();
        this.Move();
    }

    void Move()
    {
        LeanTween.moveX(gameObject, -(gameObject.transform.position.x), duration).setLoopType(loopType);
    }
}
