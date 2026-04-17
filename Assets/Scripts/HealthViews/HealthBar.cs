using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        if (_health == null)
        {
            return;
        }

        _slider.maxValue = _health.MaxValue;
        _slider.minValue = 0f;

        _health.ValueChanged += ShowHealth;

        ShowHealth(_health.CurrentHealth);
    }

    private void OnDisable()
    {
        _health.ValueChanged -= ShowHealth;
    }

    private void ShowHealth(int health)
    {
        if (_health == null)
        {
            return;
        }

        _slider.value = (float)health;
    }
}