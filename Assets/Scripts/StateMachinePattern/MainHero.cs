using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHero : MonoBehaviour
{
    [HideInInspector] public Animator _animator;
    public float _jumpForce = 11f;
    private StateMachine _stateMachine;
    public bool _isGrounded = true;
    public Rigidbody2D _body;
    public float MovementX;
    public float MoveForce = 100f;

    #region States
    private IdleState _idleState;
    private RunState _runState;
    private JumpState _jumpState;
    private DeadState _deadState;
    private GetDamageState _getDamageState;
    private AttackState _attackState;
    #endregion

    #region Tags
    private string _groundTag = "Ground";
    private string _enemyTag = "Enemy";
    #endregion

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _stateMachine = new StateMachine();

        _jumpState = new JumpState(this);
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
            _isGrounded = false;
            _stateMachine.ChangeState(_jumpState);
            
            //_stateMachine.ChangeState(_idleState);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_groundTag))
        {
            _isGrounded = true;
        }
        /*if (collision.gameObject.CompareTag(_enemyTag))
        {
            _anim.SetBool(_deathAnimation, true);
            //Destroy(gameObject);
        }*/
    }

    void Update()
    {
        //_stateMachine.CurrentState.Update();

    }
}
