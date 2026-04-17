using TMPro;
using UnityEngine;

public class HealthViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Health _health;
    [SerializeField] private string _textBeforeValue = "HP: ";
    [SerializeField] private string _valueSeparator = "/";

    private void Start()
    {
        if (_health == null)
        {
            return;
        }

        ShowHealth(_health.CurrentHealth);
        _health.ValueChanged += ShowHealth;
    }

    private void OnDisable()
    {
        _health.ValueChanged -= ShowHealth;
    }

    public void SetHealth(Health health)
    {
        if (_health != null)
        {
            _health.ValueChanged -= ShowHealth;
        }

        _health = health;
        _health.ValueChanged += ShowHealth;

        ShowHealth(_health.CurrentHealth);
    }

    private void ShowHealth(int health)
    {
        if (_health == null)
        {
            return;
        }

        _text.text = _textBeforeValue + health.ToString() + _valueSeparator + _health.MaxValue;
    }
}