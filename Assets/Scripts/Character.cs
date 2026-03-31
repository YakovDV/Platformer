using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class Character : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    private static readonly int IsDying = Animator.StringToHash("IsDying");
    private static readonly int Speed = Animator.StringToHash("Speed");

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 8f;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private LayerMask _killers;

    private Vector2 _inputDirection;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private bool _isGrounded;
    private bool _isJumping = false;
    private bool _isDead = false;

    private KeyCode _jumpKey = KeyCode.UpArrow;

    public event Action<bool> ImDead;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_isDead)
        {
            return;
        }

        ReadInput();
    }

    private void FixedUpdate()
    {
        if (_isDead)
        {
            HandleDeath();
            return;
        }

        CheckKillers();
        CheckGround();

        Vector2 horizontalVelocity = _inputDirection * _speed;
        _rigidbody.velocity = new(horizontalVelocity.x, _rigidbody.velocity.y);

        if (_isGrounded == true && _rigidbody.velocity.x > 0)
        {
            ImWalking?.Invoke(true);
        }

        if (_isJumping == true && _isGrounded == true)
        {
            Jump();
        }

        _animator.SetBool(IsJumping, !_isGrounded);

        float animationSpeed = horizontalVelocity.magnitude / _speed;
        _animator.SetFloat(Speed, animationSpeed, 0.1f, Time.fixedDeltaTime);

        SetRotation();
    }

    private void ReadInput()
    {
        float xDirection = Input.GetAxis(Horizontal);

        if (Input.GetKeyDown(_jumpKey))
        {
            _isJumping = true;
        }

        _inputDirection = new Vector2(xDirection, 0);
        _inputDirection = Vector2.ClampMagnitude(_inputDirection, 1f);
    }

    private void Jump()
    {
        Vector2 jumpDirection = new(0, _jumpForce);

        _rigidbody.AddForce(jumpDirection, ForceMode2D.Impulse);

        _isJumping = false;
        _isGrounded = false;

        ImJumping?.Invoke(true);
    }

    private void CheckGround()
    {
        Vector2 boxCastOrigin = new(_collider.bounds.center.x, _collider.bounds.min.y);
        Vector2 boxCastDirection = new(0, -1);
        Vector2 boxCastSize = new(_collider.bounds.size.x, _collider.bounds.size.y * 0.1f);

        _isGrounded = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0, boxCastDirection, 0, _ground);
    }

    private void CheckKillers()
    {
        _isDead = Physics2D.OverlapCapsule(_collider.bounds.center, _collider.bounds.size, 0, 0, _killers);
    }

    private void SetRotation()
    {
        if (_inputDirection.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_inputDirection.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }

    private void HandleDeath()
    {
        _animator.SetBool(IsDying, _isDead);
        ImDead?.Invoke(true);
    }
}