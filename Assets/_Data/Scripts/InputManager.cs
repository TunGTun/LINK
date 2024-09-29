using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : LinkMonoBehaviour
{
    [Header("InputManager")]

    [SerializeField] protected float _moveControl;
    public float MoveControl { get => _moveControl; }

    [SerializeField] protected bool _jumpControl;
    public bool JumpControl { get => _jumpControl; }

    [SerializeField] protected bool _attackControl;
    public bool AttackControl { get => _attackControl; }
    
    [SerializeField] protected bool _dashControl;
    public bool DashControl { get => _dashControl; }

    void Update()
    {
        this.CheckMoveControl();
        this.CheckJumpControl();
        this.CheckAttackControl();
        this.CheckDashControl();
    }

    protected void CheckMoveControl()
    {
        this._moveControl = Input.GetAxis("Horizontal");
    }

    protected void CheckJumpControl()
    {
        this._jumpControl = Input.GetKey(KeyCode.K);
    }

    protected void CheckAttackControl()
    {
        this._jumpControl = Input.GetKey(KeyCode.J);
    }

    protected void CheckDashControl()
    {
        this._jumpControl = Input.GetKey(KeyCode.J);
    }
}
