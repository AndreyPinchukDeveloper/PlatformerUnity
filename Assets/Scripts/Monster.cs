using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    #region AnimationsNamesFields
    private string _idleAnimation = "Idle";
    #endregion

    [SerializeField] private float _moveForce = 100f;
    [SerializeField] private float _jumpForce = 11f;

    [HideInInspector] public float _movementX;
    private bool _isGrounded = true;

    private SpriteRenderer _spriteRenderer;

    private Rigidbody2D _body;
    private BoxCollider2D _box;
    private Animator _anim;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();
        _box = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        _body.velocity = new Vector2(_movementX, _body.velocity.y);
    }
}
