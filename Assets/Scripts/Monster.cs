using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Monster : MonoBehaviour
{
    PlayerScript playerPower;

    #region AnimationsNamesFields
    private string _idleAnimation = "Idle";
    #endregion

    [SerializeField] private float _moveForce = 100f;
    [SerializeField] private float _jumpForce = 11f;

    #region Tags
    [HideInInspector] public string _groundTag = "Ground";
    [HideInInspector] public string _enemyTag = "Enemy";
    [HideInInspector] public string _playerTag = "Player";
    #endregion

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_playerTag) && playerPower._currentState == "AttackAnimation")
        {
            GameObject.Destroy(gameObject);
        }
    }
}
