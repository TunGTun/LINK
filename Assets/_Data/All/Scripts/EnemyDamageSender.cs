using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageSender : DamageSender
{
    [SerializeField] protected Collider2D _Collider2D;

    protected override void OnImpact()
    {
        Debug.Log("Impact");
    }
}
