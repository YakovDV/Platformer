using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class EnemyMover : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");

    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed;
    [SerializeField] private float _waitOnPointTime = 1f;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _direction;

    private Coroutine _waitOnPointCoroutine;
    private bool _isWaiting;

    private int _currentPointIndex = 0;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        float animationSpeed = _rigidbody.velocity.magnitude / _speed;
        _animator.SetFloat(Speed, animationSpeed, 0.1f, Time.fixedDeltaTime);

        if (_points == null || _points.Length == 0)
        {
            return;
        }

        if (_isWaiting == true)
        {
            StopMoving();
            return;
        }

        if (IsTargetReached(_points[_currentPointIndex]))
        {
            if (_waitOnPointCoroutine == null)
            {
                _waitOnPointCoroutine = StartCoroutine(WaitOnPoint());
            }

            StopMoving();
            return;
        }

        SetDirection();
        MoveToPoit();
    }

    private void MoveToPoit()
    {
        Vector2 velocity = _direction * _speed;
        velocity.y = _rigidbody.velocity.y;

        _rigidbody.velocity = velocity;
    }

    private IEnumerator WaitOnPoint()
    {
        _isWaiting = true;

        yield return new WaitForSecondsRealtime(_waitOnPointTime);

        SetNextPoint();

        _isWaiting = false;
        _waitOnPointCoroutine = null;
    }

    private bool IsTargetReached(Transform target)
    {
        float distance = 1f;

        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = target.position;

        return (targetPosition - currentPosition).sqrMagnitude <= distance * distance;
    }

    private void SetNextPoint()
    {
        _currentPointIndex++;

        if (_currentPointIndex >= _points.Length)
        {
            _currentPointIndex = 0;
        }
    }

    private void SetDirection()
    {
        Transform point = _points[_currentPointIndex];
        _direction = (point.position - transform.position).normalized;

        SetRotation();
    }

    private void SetRotation()
    {
        if (_rigidbody.velocity.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_rigidbody.velocity.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }

    private void StopMoving()
    {
        _rigidbody.velocity = new Vector2(0f, _rigidbody.velocity.y);
    }
}