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
    [SerializeField] protected float _sensitivity = 0.1f;

	[SerializeField] protected float _moveInput;
	public float MoveInput { get => _moveInput; }

	[SerializeField] protected bool _startJumpInput;
    public bool StartJumpInput { get => _startJumpInput; }

    [SerializeField] protected bool _endJumpInput;
    public bool EndJumpInput { get => _endJumpInput; }
    
    [SerializeField] protected bool _dashInput;
    public bool DashInput { get => _dashInput; }

    [SerializeField] protected bool _invisibleInput;
    public bool InvisibleInput { get => _invisibleInput; }

    public bool InputAllowed { get; set; }

    protected override void Awake()
    {
        base.Awake();
        if (InputManager._instance != null) Debug.LogError("Only 1 InputManager allow to exist");
        InputManager._instance = this;
    }

    protected override void Start()
    {
        base.Start();
        this.InputAllowed = true;
    }

    void Update()
    {
        if (!this.InputAllowed) 
        {
            this._moveInput = 0;
            this._moveAccelInput = 0;
            return;
        }
        this.CheckMoveInput();
        this.CheckJumpInput();
        this.CheckDashInput();
        this.CheckInvisibleInput();
    }

    protected virtual void CheckMoveInput()
    {
        //Kiểm tra nhấn nút A, D khi cả 2 nút cùng được nhấn

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
			_moveAccelInput = Mathf.Lerp(_moveAccelInput, 0f, Time.fixedDeltaTime * 3 / _sensitivity);
            _moveInput = 0;
            _lastKeyPressed = KeyCode.LeftArrow;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
			_moveAccelInput = Mathf.Lerp(_moveAccelInput, 0f, Time.fixedDeltaTime * 3 / _sensitivity);
            _moveInput = 0;
            _lastKeyPressed = KeyCode.RightArrow;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)) _lastKeyPressed = KeyCode.RightArrow;

        if (Input.GetKeyUp(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow)) _lastKeyPressed = KeyCode.LeftArrow;

        if (_lastKeyPressed == KeyCode.LeftArrow && Input.GetKey(KeyCode.LeftArrow))
        {
            if (_moveAccelInput > -1) _moveAccelInput -= Time.fixedDeltaTime / _sensitivity;
            _moveInput = -1;
            return;
        }

        if (_lastKeyPressed == KeyCode.RightArrow && Input.GetKey(KeyCode.RightArrow))
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
        this._startJumpInput = Input.GetKeyDown(KeyCode.X);
        this._endJumpInput = Input.GetKeyUp(KeyCode.X);
    }

    protected virtual void CheckDashInput()
    {
        this._dashInput = Input.GetKeyDown(KeyCode.Z);
    }

    protected virtual void CheckInvisibleInput()
    {
        this._invisibleInput = Input.GetKeyDown(KeyCode.C);
    }
}
