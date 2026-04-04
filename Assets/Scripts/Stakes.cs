using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class Stakes : MonoBehaviour
{
    [SerializeField] private int _damage = 100;

    private Collider2D _collider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(_damage);
        }
    }
}