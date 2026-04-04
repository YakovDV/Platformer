using TMPro;
using UnityEngine;

public class HealthViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private Health _health;

    private void Start()
    {
        if (_health == null)
        {
            return;
        }

        ShowHealth(_health.CurrentHealth);
    }

    private void OnDisable()
    {
        _health.HealthChanged -= ShowHealth;
    }

    public void SetHealth(Health health)
    {
        if (_health != null)
        {
            _health.HealthChanged -= ShowHealth;
        }

        _health = health;
        _health.HealthChanged += ShowHealth;

        ShowHealth(_health.CurrentHealth);
    }

    private void ShowHealth(int health)
    {
        if (_health == null)
        {
            return;
        }

        _text.text = health.ToString();
    }
}