using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class HealthBarSmooth : MonoBehaviour
{
    [SerializeField] private float _sliderSpeed = 1.0f;
    [SerializeField] private Health _health;

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

        SetHealth(_health);
    }

    private void OnDisable()
    {
        _health.ValueChanged -= ShowHealth;
    }

    public void SetHealth(Health health)
    {
        if (_slider.gameObject.activeSelf == false)
        {
            _slider.gameObject.SetActive(true);
        }

        _health = health;

        _slider.maxValue = _health.MaxValue;
        _slider.minValue = 0f;

        _health.ValueChanged += ShowHealth;
        ShowHealth(_health.CurrentHealth);
    }

    private void ShowHealth(int health)
    {
        if (_smoothSliderCoroutine != null)
        {
            StopCoroutine(_smoothSliderCoroutine);
            _smoothSliderCoroutine = null;
        }

        _smoothSliderCoroutine = StartCoroutine(ChangeSliderSmooth((float)health));
    }

    private IEnumerator ChangeSliderSmooth(float health)
    {
        while (Mathf.Approximately(_slider.value, health) == false)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, health, _sliderSpeed * Time.deltaTime);

            yield return null;
        }

        if (health == _slider.minValue)
        {
            _slider.gameObject.SetActive(false);
        }

        _slider.value = health;
    }
}