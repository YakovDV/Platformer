using UnityEngine;

[RequireComponent(typeof(Health), typeof(Rigidbody2D))]

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;

    private Health _health;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }

    private void OnDied(bool isDied)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.bodyType = RigidbodyType2D.Static;
        _collider.enabled = false;
    }
}
