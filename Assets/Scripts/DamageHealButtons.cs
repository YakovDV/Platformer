using UnityEngine;
using UnityEngine.UI;

public class DamageHealButtons : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private int _damage = 10;
    [SerializeField] private int _heal = 10;
    [SerializeField] private Button _damageButton;
    [SerializeField] private Button _healButton;

    private void OnEnable()
    {
        _damageButton.onClick.AddListener(DealDamage);
        _healButton.onClick.AddListener(DealHeal);
    }

    public void SetTaget(Health health)
    {
        _health = health;
    }

    private void DealDamage()
    {
        if (_health == null)
        {
            return;
        }

        _health.TakeDamage(_damage);
    }

    private void DealHeal()
    {
        if (_health == null)
        {
            return;
        }

        _health.TakeHeal(_heal);
    }
}
