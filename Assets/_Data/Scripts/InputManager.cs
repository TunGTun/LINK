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
        if (InputManager._instance != null) Debug.LogError("Only 1 InputManager allow to exist");
        InputManager._instance = this;
    }

    void Update()
    {
        this.CheckMoveInput();
        this.CheckJumpInput();
        this.CheckAttackInput();
        this.CheckDashInput();
    }

    protected virtual void CheckMoveInput()
    {
        //Kiểm tra nhấn nút A, D khi cả 2 nút cùng được nhấn

        if (Input.GetKeyDown(KeyCode.A))
        {
			_moveAccelInput = Mathf.Lerp(_moveAccelInput, 0f, Time.fixedDeltaTime * 3 / _sensitivity);
            _moveInput = 0;
            _lastKeyPressed = KeyCode.A;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
			_moveAccelInput = Mathf.Lerp(_moveAccelInput, 0f, Time.fixedDeltaTime * 3 / _sensitivity);
            _moveInput = 0;
            _lastKeyPressed = KeyCode.D;
        }

        if (Input.GetKeyUp(KeyCode.A) && Input.GetKey(KeyCode.D)) _lastKeyPressed = KeyCode.D;

        if (Input.GetKeyUp(KeyCode.D) && Input.GetKey(KeyCode.A)) _lastKeyPressed = KeyCode.A;

        if (_lastKeyPressed == KeyCode.A && Input.GetKey(KeyCode.A))
        {
            if (_moveAccelInput > -1) _moveAccelInput -= Time.fixedDeltaTime / _sensitivity;
            _moveInput = -1;
            return;
        }

        if (_lastKeyPressed == KeyCode.D && Input.GetKey(KeyCode.D))
        {
            if (_moveAccelInput < 1) _moveAccelInput += Time.fixedDeltaTime / _sensitivity;
            _moveInput = 1;
            return;
        }

		_moveAccelInput = Mathf.Lerp(_moveAccelInput, 0f, Time.fixedDeltaTime * 3 / _sensitivity);
        _moveInput = 0;
    }

    protected virtual void CheckJumpInput()
    {
        this._starJumpInput = Input.GetKeyDown(KeyCode.K);
        this._endJumpInput = Input.GetKeyUp(KeyCode.K);
    }

    protected virtual void CheckAttackInput()
    {
        this._attackInput = Input.GetKey(KeyCode.J);
    }

    protected virtual void CheckDashInput()
    {
        this._dashInput = Input.GetKeyDown(KeyCode.L);
    }
}
