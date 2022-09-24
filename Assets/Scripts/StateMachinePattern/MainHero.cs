using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHero : MonoBehaviour
{
    public Animator _animator;
    [HideInInspector] public SpriteRenderer SpriteRenderer;
    public float _jumpForce = 11f;
    private StateMachine _stateMachine;
    public bool _isGrounded = true;
    public Rigidbody2D _body;
    public float MovementX;
    public float MoveForce = 40f;

    #region States
    private IdleState _idleState;
    private RunState _runState;
    private JumpState _jumpState;
    private DeadState _deadState;
    private GetDamageState _getDamageState;
    private AttackState _attackState;
    #endregion

    #region AnimationsNamesFields
    private string _walkAnimation = "Walk";
    public string RunAnimation = "Run";
    private string _idleAnimation = "Idle";
    private string _jumpAnimation = "Jump";
    private string _getDamageAnimation = "GetDamage";
    private string _attackAnimation = "Attack";
    private string _deathAnimation = "Death";
    #endregion

    #region Tags
    private string _groundTag = "Ground";
    private string _enemyTag = "Enemy";
    #endregion

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        _stateMachine = new StateMachine();

        _jumpState = new JumpState(this);
        _runState = new RunState(this);
        _deadState = new DeadState();
        _getDamageState = new GetDamageState();
        _attackState = new AttackState();
        _idleState = new IdleState();

        _stateMachine.Initialize(_idleState);
    }

    private void FixedUpdate()
    {
        _stateMachine.CurrentState.Update();

        if (Input.GetButton("Jump") && _isGrounded)
        {
            _isGrounded = false;//we need it to forbid second jump while player already jump
            _stateMachine.ChangeState(_jumpState);
        }
        if (Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.D))
        {
            _stateMachine.ChangeState(_runState);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_groundTag))
        {
            _isGrounded = true;
        }
    }

    void Update()
    {
        
    }
}
