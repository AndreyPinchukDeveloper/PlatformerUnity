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

    [SerializeField]
    private float moveForce =100f;
    [SerializeField]
    private float jumpForce = 11f;
    private float movementX;

    private SpriteRenderer _spriteRenderer;

    private Rigidbody2D _body;
    private BoxCollider2D _box;
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();
        _box = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
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
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f)* Time.deltaTime * moveForce;
    }

    private void AnimatePlayer()
    {
        //going to the right side
        if (movementX>0)
        {
            _anim.SetBool(_walkAnimation, true);
            _spriteRenderer.flipX = false;
        }
        //going to the left side
        else if (movementX<0)
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
        if (Input.GetButton("Jump"))
        {
            _body.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
}
