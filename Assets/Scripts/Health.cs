using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxValue;

    private int _minValue = 0;

    public event Action Died;
    public event Action<int> ValueChanged;

    public int CurrentHealth { get; private set; }

    public bool IsDead { get; private set; }

    private void Awake()
    {
        IsDead = false;
        CurrentHealth = _maxValue;
    }

    public void TakeDamage(int value)
    {
        if (IsDead == true)
        {
            return;
        }

        CurrentHealth -= value;

        if (CurrentHealth <= _minValue)
        {
            Died?.Invoke();
            CurrentHealth = _minValue;
            IsDead = true;
        }

        ValueChanged?.Invoke(CurrentHealth);
    }

    public void TakeHeal(int value)
    {
        if (IsDead == true)
        {
            return;
        }

        CurrentHealth += value;

        if (CurrentHealth >= _maxValue)
        {
            CurrentHealth = _maxValue;
        }

        ValueChanged?.Invoke(CurrentHealth);
    }
}
