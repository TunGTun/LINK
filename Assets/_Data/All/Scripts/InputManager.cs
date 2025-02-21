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
    [SerializeField] protected float _sensitivity = 1f;

    [SerializeField] protected float _moveInput;
    public float MoveInput { get => _moveInput; }

<<<<<<< HEAD
    [SerializeField] protected bool _starJumpInput;
    public bool StarJumpInput { get => _starJumpInput; }

    [SerializeField] protected bool _endJumpInput;
    public bool EndJumpInput { get => _endJumpInput; }

    [SerializeField] protected bool _attackInput;
    public bool AttackInput { get => _attackInput; }

=======
	[SerializeField] protected bool _startJumpInput;
    public bool StartJumpInput { get => _startJumpInput; }

    [SerializeField] protected bool _endJumpInput;
    public bool EndJumpInput { get => _endJumpInput; }
    
>>>>>>> main
    [SerializeField] protected bool _dashInput;
    public bool DashInput { get => _dashInput; }

    protected override void Awake()
    {
        base.Awake();
<<<<<<< HEAD
        if (_instance != null)
        {
            Debug.LogError("Only one InputManager is allowed to exist!");
            Destroy(gameObject);
            return;
        }
        _instance = this;
=======
        if (InputManager._instance != null) Debug.LogError("Only 1 InputManager allow to exist");
        InputManager._instance = this;
        //DontDestroyOnLoad(this);
>>>>>>> main
    }

    void Update()
    {
<<<<<<< HEAD
        CheckMoveInput();
        CheckJumpInput();
        CheckAttackInput();
        CheckDashInput();
=======
        this.CheckMoveInput();
        this.CheckJumpInput();
        this.CheckDashInput();
>>>>>>> main
    }

    protected virtual void CheckMoveInput()
    {
        bool rightPressed = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        bool leftPressed = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);

<<<<<<< HEAD
        if (rightPressed && !leftPressed)
=======
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
			_moveAccelInput = Mathf.Lerp(_moveAccelInput, 0f, Time.fixedDeltaTime * 3 / _sensitivity);
            _moveInput = 0;
            _lastKeyPressed = KeyCode.LeftArrow;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
>>>>>>> main
        {
            _moveAccelInput = Mathf.Clamp(_moveAccelInput + Time.deltaTime / _sensitivity, -1f, 1f);
            _moveInput = 1;
            _lastKeyPressed = KeyCode.RightArrow;
        }
<<<<<<< HEAD
        else if (leftPressed && !rightPressed)
        {
            _moveAccelInput = Mathf.Clamp(_moveAccelInput - Time.deltaTime / _sensitivity, -1f, 1f);
=======

        if (Input.GetKeyUp(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)) _lastKeyPressed = KeyCode.RightArrow;

        if (Input.GetKeyUp(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow)) _lastKeyPressed = KeyCode.LeftArrow;

        if (_lastKeyPressed == KeyCode.LeftArrow && Input.GetKey(KeyCode.LeftArrow))
        {
            if (_moveAccelInput > -1) _moveAccelInput -= Time.fixedDeltaTime / _sensitivity;
>>>>>>> main
            _moveInput = -1;
            _lastKeyPressed = KeyCode.LeftArrow;
        }
<<<<<<< HEAD
        else
=======

        if (_lastKeyPressed == KeyCode.RightArrow && Input.GetKey(KeyCode.RightArrow))
>>>>>>> main
        {
            _moveAccelInput = Mathf.Lerp(_moveAccelInput, 0f, Time.deltaTime * 3 / _sensitivity);
            _moveInput = 0;
        }
    }

    protected virtual void CheckJumpInput()
    {
<<<<<<< HEAD
        _starJumpInput = Input.GetKeyDown(KeyCode.K);
        _endJumpInput = Input.GetKeyUp(KeyCode.K);
    }

    protected virtual void CheckAttackInput()
    {
        _attackInput = Input.GetKey(KeyCode.J);
=======
        this._startJumpInput = Input.GetKeyDown(KeyCode.X);
        this._endJumpInput = Input.GetKeyUp(KeyCode.X);
>>>>>>> main
    }

    protected virtual void CheckDashInput()
    {
<<<<<<< HEAD
        _dashInput = Input.GetKeyDown(KeyCode.L);
=======
        this._dashInput = Input.GetKeyDown(KeyCode.Z);
>>>>>>> main
    }
}