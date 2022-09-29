using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float _runSpeed = 5f;
    [SerializeField] private float _attackDelay = 0.3f;
    [SerializeField] private float _getDamageDelay = 0.4f;
    [SerializeField] private float _jumpForce = 850;
    private float _xAxis;
    private float _yAxis;

    #region Tags
    [HideInInspector] public string _groundTag = "Ground";
    [HideInInspector] public string _enemyTag = "Enemy";
    #endregion

    private int _groundMask;

    private Animator _animator;
    private Rigidbody2D _body;

    private bool _isJumpPressed;
    private bool _isGrounded;
    private bool _isAttackPressed;
    [HideInInspector] public bool _isAttacking;
    private bool _isDamaged;

    [HideInInspector] public string _currentState;

    #region States
    const string PLAYER_IDLE = "Idle";
    const string PLAYER_RUN = "Run";
    const string PLAYER_JUMP = "Jump";
    const string PLAYER_GETDAMAGE = "GetDamage";
    const string PLAYER_ATTACK = "AttackAnimation";
    const string PLAYER_DEATH = "Death";
    #endregion

    void Start()
    {
        _animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();
        //we can write it as
        //_groundMask = 1 * (int)Math.Pow(2, 4);
        //but << is more than 50% faster(66%)
        _groundMask = 1 << LayerMask.NameToLayer("Ground");
        
    }

    //Update only for input(buttons)
    void Update()
    {
        _xAxis = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isJumpPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _isAttackPressed = true;
        }
    }

    //FU for calculating physics
    private void FixedUpdate()
    {
        if (!_isDamaged)
        {
            //is player on the ground ?
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, _groundMask);

            if (_isGrounded != null)
            {
                _isGrounded = true;
            }
            else _isGrounded = false;

            //-------------------------------
            //movement update based on unput
            

            //------------------------
            //trying to jump
            if (_isJumpPressed && _isGrounded)
            {
                _body.AddForce(new Vector2(0, _jumpForce));
                _isJumpPressed = false;
                ChangeAnimationState(PLAYER_JUMP);
            }

            _body.velocity = vel;

            //-----------------
            //trying to attack
            if (_isAttackPressed)
            {
                _isAttackPressed = false;

                if (!_isAttacking)
                {
                    _isAttacking = true;
                    ChangeAnimationState(PLAYER_ATTACK);
                }

                _attackDelay = _animator.GetCurrentAnimatorStateInfo(0).length+1;
                Invoke("AttackComplete", _attackDelay);
            }
        }
        else//we need this code to GetDamage animation won't be interrupted
        {
            _getDamageDelay = _animator.GetCurrentAnimatorStateInfo(0).length;
            Invoke("GetDamageComplete", _getDamageDelay);
        } 
    }

    private void AttackComplete()
    {
        _isAttacking = false;
    }
    private void GetDamageComplete()
    {
        _isDamaged = false;
    }

    private void ChangeAnimationState(string newState)
    {
        //stop the same animation from interrupting itself
        if (_currentState == newState) return;

        _currentState = newState;
        _animator.Play(newState);

        //reassign the current state
        _currentState = newState;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_groundTag))
        {
            _isGrounded = true;
        }
        if (collision.gameObject.CompareTag(_enemyTag))
        {
            HealthManager.health --;
            _isDamaged = true;
            if (HealthManager.health<=0)
            {
                ChangeAnimationState(PLAYER_DEATH);
                _body.mass = 100;
                _body = null;
                _animator = null;
            }
            else
            {
                ChangeAnimationState(PLAYER_GETDAMAGE);

                StartCoroutine(GetHurt());
            }
        }
    }

    IEnumerator GetHurt()
    {
        Physics2D.IgnoreLayerCollision(6, 8);
        yield return new WaitForSeconds(3);
        Physics2D.IgnoreLayerCollision(6, 8, false);
    }

    /*#region LastVersion
    [SerializeField] private float _runSpeed = 5f;
    [SerializeField] private float _attackDelay = 0.3f;
    [SerializeField] private float _getDamageDelay = 0.4f;
    [SerializeField] private float _jumpForce = 850;
    private float _xAxis;
    private float _yAxis;

    #region Tags
    [HideInInspector] public string _groundTag = "Ground";
    [HideInInspector] public string _enemyTag = "Enemy";
    #endregion

    private int _groundMask;

    private Animator _animator;
    private Rigidbody2D _body;

    private bool _isJumpPressed;
    private bool _isGrounded;
    private bool _isAttackPressed;
    [HideInInspector] public bool _isAttacking;
    private bool _isDamaged;

    [HideInInspector] public string _currentState;

    #region States
    const string PLAYER_IDLE = "Idle";
    const string PLAYER_RUN = "Run";
    const string PLAYER_JUMP = "Jump";
    const string PLAYER_GETDAMAGE = "GetDamage";
    const string PLAYER_ATTACK = "AttackAnimation";
    const string PLAYER_DEATH = "Death";
    #endregion

    void Start()
    {
        _animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();
        //we can write it as
        //_groundMask = 1 * (int)Math.Pow(2, 4);
        //but << is more than 50% faster(66%)
        _groundMask = 1 << LayerMask.NameToLayer("Ground");

    }

    //Update only for input(buttons)
    void Update()
    {
        _xAxis = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isJumpPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _isAttackPressed = true;
        }
    }

    //FU for calculating physics
    private void FixedUpdate()
    {
        if (!_isDamaged)
        {
            //is player on the ground ?
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, _groundMask);

            if (_isGrounded != null)
            {
                _isGrounded = true;
            }
            else _isGrounded = false;

            //-------------------------------
            //movement update based on unput
            Vector2 vel = new Vector2(0, _body.velocity.y);

            if (_xAxis < 0)
            {
                vel.x = -_runSpeed;
                transform.localScale = new Vector2(-1, 1);
                ChangeAnimationState(PLAYER_RUN);
            }
            else if (_xAxis > 0)
            {
                vel.x = _runSpeed;
                transform.localScale = new Vector2(1, 1);
                ChangeAnimationState(PLAYER_RUN);
            }
            else vel.x = 0;

            if (_isGrounded && !_isAttacking)//this condition need for animations don't override themself
            {
                if (_xAxis != 0)
                {
                    ChangeAnimationState(PLAYER_RUN);
                }
                else ChangeAnimationState(PLAYER_IDLE);
            }

            //------------------------
            //trying to jump
            if (_isJumpPressed && _isGrounded)
            {
                _body.AddForce(new Vector2(0, _jumpForce));
                _isJumpPressed = false;
                ChangeAnimationState(PLAYER_JUMP);
            }

            _body.velocity = vel;

            //-----------------
            //trying to attack
            if (_isAttackPressed)
            {
                _isAttackPressed = false;

                if (!_isAttacking)
                {
                    _isAttacking = true;
                    ChangeAnimationState(PLAYER_ATTACK);
                }

                _attackDelay = _animator.GetCurrentAnimatorStateInfo(0).length + 1;
                Invoke("AttackComplete", _attackDelay);
            }
        }
        else//we need this code to GetDamage animation won't be interrupted
        {
            _getDamageDelay = _animator.GetCurrentAnimatorStateInfo(0).length;
            Invoke("GetDamageComplete", _getDamageDelay);
        }
    }

    private void AttackComplete()
    {
        _isAttacking = false;
    }
    private void GetDamageComplete()
    {
        _isDamaged = false;
    }

    private void ChangeAnimationState(string newState)
    {
        //stop the same animation from interrupting itself
        if (_currentState == newState) return;

        _currentState = newState;
        _animator.Play(newState);

        //reassign the current state
        _currentState = newState;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_groundTag))
        {
            _isGrounded = true;
        }
        if (collision.gameObject.CompareTag(_enemyTag))
        {
            HealthManager.health--;
            _isDamaged = true;
            if (HealthManager.health <= 0)
            {
                ChangeAnimationState(PLAYER_DEATH);
                _body.mass = 100;
                _body = null;
                _animator = null;
            }
            else
            {
                ChangeAnimationState(PLAYER_GETDAMAGE);

                StartCoroutine(GetHurt());
            }
        }
    }

    IEnumerator GetHurt()
    {
        Physics2D.IgnoreLayerCollision(6, 8);
        yield return new WaitForSeconds(3);
        Physics2D.IgnoreLayerCollision(6, 8, false);
    }
    #endregion*/
}
