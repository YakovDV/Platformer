using UnityEngine;

[RequireComponent(typeof(Health), typeof(Rigidbody2D), typeof(EnemyMover))]

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;
    [SerializeField] private EnemyAnimator _enemyAnimator;

    private Health _health;
    private Rigidbody2D _rigidbody;
    private EnemyPatrol _enemyPatrol;
    private EnemyMover _enemyMover;
    private EnemyAttack _enemyAttack;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _enemyPatrol = GetComponent<EnemyPatrol>();
        _enemyMover = GetComponent<EnemyMover>();
        _enemyAttack = GetComponent<EnemyAttack>();
    }

    private void OnEnable()
    {
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }

    private void OnDied()
    {
        _enemyAnimator.PlayDeathAnimation();

        _enemyPatrol.enabled = false;
        _enemyMover.enabled = false;
        _enemyAttack.enabled = false;

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.bodyType = RigidbodyType2D.Static;
        _collider.enabled = false;
    }
}
