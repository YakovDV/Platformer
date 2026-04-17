using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class HealthBarSmooth : MonoBehaviour
{
    [SerializeField] private float _sliderSpeed = 1.0f;
    [SerializeField] private Health _health;

    private float _valueScaler = 100f;
    private Slider _slider;
    private Coroutine _smoothSliderCoroutine;

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

        _slider.maxValue = _health.MaxValue / _valueScaler;
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
        float currentHealth = health / _valueScaler;

        if (_smoothSliderCoroutine != null)
        {
            StopCoroutine(_smoothSliderCoroutine);
            _smoothSliderCoroutine = null;
        }

        _smoothSliderCoroutine = StartCoroutine(ChangeSliderSmooth(currentHealth));
    }

    private IEnumerator ChangeSliderSmooth(float health)
    {
        while (Mathf.Approximately(_slider.value, health) == false)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, health, _sliderSpeed * Time.deltaTime);

            yield return null;
        }

        _slider.value = health;
    }
}