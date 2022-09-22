using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region AnimationsNamesFields
    private string _walkAnimation = "Walk";
    private string _runAnimation = "Run";
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


    [SerializeField] private float _moveForce =100f;
    [SerializeField] private float _jumpForce = 11f;

    private float _movementX;
    private bool _isGrounded = true;

    private SpriteRenderer _spriteRenderer;

    private Rigidbody2D _body;
    private BoxCollider2D _box;

    #region StateMachine
    [HideInInspector] public Animator _anim;
    #endregion


    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();
        _box = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        PlayerMoveKeyBoard();
        AnimatePlayer();
    }

    /// <summary>
    /// All calculation with physic put here
    /// </summary>
    private void FixedUpdate()
    {
        Jump();
    }

    /// <summary>
    /// Left and right movement
    /// </summary>
    
    private void PlayerMoveKeyBoard()
    {
        _movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(_movementX, 0f, 0f)* Time.deltaTime * _moveForce;
    }

    private void AnimatePlayer()
    {
        //going to the right side
        if (_movementX>0)
        {
            _anim.SetBool(_walkAnimation, true);
            _spriteRenderer.flipX = false;
        }
        //going to the left side
        else if (_movementX<0)
        {
            _anim.SetBool(_walkAnimation, true);
            _spriteRenderer.flipX = true;
        }
        else
        {
            _anim.SetBool(_walkAnimation, false);
        }
    }

    private void Jump()
    {
        if (Input.GetButton("Jump")&&_isGrounded)
        {
            _isGrounded = false;//we need it to forbid second jump while player already jump
            _body.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// This method need us to give the player opportunity to jump again after method Jump()
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_groundTag))
        {
            _isGrounded = true;
        }
        if (collision.gameObject.CompareTag(_enemyTag))
        {
            _anim.SetBool(_deathAnimation, true);
            //Destroy(gameObject);
        }
    }
}
