using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;
    private EnemyPatrol _enemyPatrol;
    private Rotator _horizontalTurn;
    private Vector2 _direction;
    private Transform _currentTarget;

    public float NormalizedHorizontalSpeed
    {
        get
        {
            return new Vector2(_rigidbody.velocity.x, 0f).magnitude / _speed;
        }
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _enemyPatrol = GetComponent<EnemyPatrol>();
        _horizontalTurn = GetComponent<Rotator>();
    }

    private void FixedUpdate()
    {
        Transform target = _enemyPatrol.CurrentPoint;

        if (_enemyPatrol.IsWaiting == true || target == null)
        {
            StopMoving();
            return;
        }

        if (_currentTarget != target)
        {
            SetDirection(target);
            MoveToPoint();
        }
    }

    private void SetDirection(Transform target)
    {
        _currentTarget = target;
        _direction = (_currentTarget.position - transform.position).normalized;
        _horizontalTurn.TurnToMovement(_direction.x);
    }

    private void MoveToPoint()
    {
        Vector2 velocity = _direction * _speed;
        velocity.y = _rigidbody.velocity.y;

        _rigidbody.velocity = velocity;
    }

    private void StopMoving()
    {
        _rigidbody.velocity = new Vector2(0f, _rigidbody.velocity.y);
    }
}