using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;
    private EnemyPatrol _enemyPatrol;
    private HorizontalTurn2D _horizontalTurn;
    private Vector2 _direction;

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
        _horizontalTurn = GetComponent<HorizontalTurn2D>();
    }

    private void FixedUpdate()
    {
        Transform target = _enemyPatrol.CurrentPoint;

        if (_enemyPatrol.IsWaiting == true || target == null)
        {
            StopMoving();
            return;
        }

        SetDirection(target);
        _horizontalTurn.TurnToMovement(_direction.x);

        MoveToPoint();
    }

    private void SetDirection(Transform point)
    {
        _direction = (point.position - transform.position).normalized;
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