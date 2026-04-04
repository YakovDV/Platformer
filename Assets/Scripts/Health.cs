using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    private int _minHealth = 0;

    public event Action<bool> Died;
    public event Action<int> HealthChanged;

    public int CurrentHealth { get; private set; }

    public bool IsDead { get; private set; }

    private void Awake()
    {
        IsDead = false;
        CurrentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (IsDead == true)
        {
            return;
        }

        CurrentHealth -= damage;

        if (CurrentHealth <= _minHealth)
        {
            Died?.Invoke(true);
            CurrentHealth = _minHealth;
            IsDead = true;
        }

        HealthChanged?.Invoke(CurrentHealth);
    }

    public void TakeHeal(int heal)
    {
        if (IsDead == true)
        {
            return;
        }

        CurrentHealth += heal;

        if (CurrentHealth >= _maxHealth)
        {
            CurrentHealth = _maxHealth;
        }

        HealthChanged?.Invoke(CurrentHealth);
    }
}
