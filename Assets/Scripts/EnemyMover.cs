using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody2D), typeof(EnemyPatrol), typeof(Rotator))]

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _chaseSpeed;
    [SerializeField] private EnemyCharacterSensor _characterSensor;

    private Rigidbody2D _rigidbody;
    private Health _health;
    private EnemyPatrol _enemyPatrol;
    private Rotator _horizontalTurn;
    private Vector2 _direction;
    private Transform _currentTarget;

    private float _cahseDistance = 2f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _enemyPatrol = GetComponent<EnemyPatrol>();
        _horizontalTurn = GetComponent<Rotator>();
        _health = GetComponent<Health>();
    }

    private void FixedUpdate()
    {
        if (_health != null && _health.IsDead == true)
        {
            return;
        }

        Transform target = _characterSensor.TargetPosition;

        if (target != null)
        {
            Chase(target);
            return;
        }

        target = _enemyPatrol.CurrentPoint;

        if (_enemyPatrol.IsWaiting == true || target == null)
        {
            StopMoving();
            return;
        }

        if (_currentTarget != target)
        {
            SetDirection(target);
            MoveToPoint(_speed);
        }
    }

    public float NormalizedHorizontalSpeed()
    {
        return new Vector2(_rigidbody.velocity.x, 0f).magnitude / _speed;
    }

    private void SetDirection(Transform target)
    {
        _currentTarget = target;
        _direction = (_currentTarget.position - transform.position).normalized;
        _horizontalTurn.TurnToMovement(_direction.x);
    }

    private void MoveToPoint(float speed)
    {
        Vector2 velocity = _direction * speed;
        velocity.y = _rigidbody.velocity.y;

        _rigidbody.velocity = velocity;
    }

    private void Chase(Transform target)
    {
        Vector2 toTarget = (Vector2)target.position - (Vector2)transform.position;
        float distanceSqr = _cahseDistance * _cahseDistance;

        if (toTarget.sqrMagnitude <= distanceSqr)
        {
            StopMoving();
        }
        else
        {
            SetDirection(target);
            MoveToPoint(_chaseSpeed);
        }
    }

    private void StopMoving()
    {
        _rigidbody.velocity = new Vector2(0f, _rigidbody.velocity.y);
    }
}