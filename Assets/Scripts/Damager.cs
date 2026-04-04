using System;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private Vector2 _attackSize = new(0.4f, 0.4f);

    private Health _ownHealth;

    private void Awake()
    {
        _ownHealth = GetComponentInParent<Health>();
    }

    public void DamageDeal()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, _attackSize, 0f);

        foreach (Collider2D hit in hits)
        {
            Health targetHealth = hit.GetComponentInParent<Health>();

            if (targetHealth == null)
                continue;

            if (targetHealth == _ownHealth)
                continue;

            targetHealth.TakeDamage(_damage);
            break;
        }
    }
}