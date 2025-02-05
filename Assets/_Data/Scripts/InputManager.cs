using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : LinkMonoBehaviour
{
    private static InputManager _instance;
    public static InputManager Instance { get => _instance; }

    [Header("InputManager")]
    protected KeyCode _lastKeyPressed;

    [SerializeField] protected float _moveAccelInput;
    public float MoveAccelInput { get => _moveAccelInput; }
    [SerializeField] protected float _sensitivity = 2f;

    [SerializeField] protected float _moveInput;
    public float MoveInput { get => _moveInput; }

    [SerializeField] protected bool _starJumpInput;
    public bool StarJumpInput { get => _starJumpInput; }

    [SerializeField] protected bool _endJumpInput;
    public bool EndJumpInput { get => _endJumpInput; }

    [SerializeField] protected bool _attackInput;
    public bool AttackInput { get => _attackInput; }

    [SerializeField] protected bool _dashInput;
    public bool DashInput { get => _dashInput; }

    protected override void Awake()
    {
        base.Awake();
        if (_instance != null)
        {
            Debug.LogError("Only one InputManager is allowed to exist!");
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    void Update()
    {
        CheckMoveInput();
        CheckJumpInput();
        CheckAttackInput();
        CheckDashInput();
    }

    protected virtual void CheckMoveInput()
    {
        bool rightPressed = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        bool leftPressed = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);

        if (rightPressed && !leftPressed)
        {
            _moveAccelInput = Mathf.Clamp(_moveAccelInput + Time.deltaTime / _sensitivity, -1f, 1f);
            _moveInput = 1;
            _lastKeyPressed = KeyCode.RightArrow;
        }
        else if (leftPressed && !rightPressed)
        {
            _moveAccelInput = Mathf.Clamp(_moveAccelInput - Time.deltaTime / _sensitivity, -1f, 1f);
            _moveInput = -1;
            _lastKeyPressed = KeyCode.LeftArrow;
        }
        else
        {
            _moveAccelInput = Mathf.Lerp(_moveAccelInput, 0f, Time.deltaTime * 3 / _sensitivity);
            _moveInput = 0;
        }
    }

    protected virtual void CheckJumpInput()
    {
        _starJumpInput = Input.GetKeyDown(KeyCode.K);
        _endJumpInput = Input.GetKeyUp(KeyCode.K);
    }

    protected virtual void CheckAttackInput()
    {
        _attackInput = Input.GetKey(KeyCode.J);
    }

    protected virtual void CheckDashInput()
    {
        _dashInput = Input.GetKeyDown(KeyCode.L);
    }
}