using UnityEngine;

[RequireComponent (typeof(Collider2D))]

public class Healpack : MonoBehaviour
{
    [SerializeField] private int _heal = 10;

    private Collider2D _collider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Health>(out Health health))
        {
            health.TakeHeal(_heal);
            Destroy(this.gameObject);
        }
    }
}