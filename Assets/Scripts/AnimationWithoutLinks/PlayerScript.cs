using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _attackDelay = 0.3f;
    [SerializeField] private float _jumpForce = 850;
    private float _xAxis;
    private float _yAxis;

    private int _groundMask;

    private Animator _animator;
    private Rigidbody2D _body;

    private bool _isJumpPressed;
    private bool _isGrounded;
    private bool _isAttackPressed;
    private bool _isAttacking;

    private string _currentState;

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

        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            _isAttackPressed = true;
        }
    }

    //FU for calculating physics
    private void FixedUpdate()
    {
        //is player on the ground ?
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, _groundMask);
        _isGrounded = hit.collider != null ? true : false;


        //-------------------------------
        //movement update based on unput
        Vector2 vel = new Vector2(0, _body.velocity.y);

        if (_xAxis <0)
        {
            vel.x = -_walkSpeed;
            transform.localScale = new Vector2(-1, 1);
            ChangeAnimationState(PLAYER_RUN);
        }
        else if (_xAxis > 0)
        {
            vel.x = _walkSpeed;
            transform.localScale = new Vector2(1, 1);
            ChangeAnimationState(PLAYER_RUN);
        }
        else
        {
            vel.x = 0;
            
        }

        if (_xAxis != 0)
        {
            ChangeAnimationState(PLAYER_RUN);
        }
        else ChangeAnimationState(PLAYER_IDLE);


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
            }
        }
    }

    private void ChangeAnimationState(string newState)
    {
        //stop the same animation from interrupting itself
        if (_currentState == newState) return;

        _animator.Play(newState);

        //reassign the current state
        _currentState = newState;
    }
}
