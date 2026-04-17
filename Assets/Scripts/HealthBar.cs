using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class HealthBar : MonoBehaviour
{
    [SerializeField] private bool _smoothSlider = false;
    [SerializeField] private bool _isTracking = false;
    [SerializeField] private float _sliderSpeed = 1.0f;
    [SerializeField] private Health _health;
    [SerializeField] private GameObject _trackingTarget;
    [SerializeField] private Vector3 _positionOffset;

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

        _slider.maxValue = _health.MaxValue / 100f;
        _slider.minValue = 0f;

        _health.ValueChanged += ShowHealth;

        ShowHealth(_health.CurrentHealth);
    }

    private void Update()
    {
        if (_isTracking == false)
        {
            return;
        }

        Vector3 position = new(_trackingTarget.transform.position.x, _trackingTarget.transform.position.y, 0f);

        transform.position = position + _positionOffset;
    }

    private void OnDisable()
    {
        _health.ValueChanged -= ShowHealth;
    }

    public void SetCharacter(Character character)
    {
        if (character.TryGetComponent<Health>(out Health health))
        {
            _health = health;
            _trackingTarget = character.gameObject;

            _health.ValueChanged += ShowHealth;

            ShowHealth(_health.CurrentHealth);
        }
    }

    private void ShowHealth(int health)
    {
        float currentHealth = health / 100f;

        if (_smoothSlider == true)
        {
            if (_smoothSliderCoroutine != null)
            {
                StopCoroutine(_smoothSliderCoroutine);
                _smoothSliderCoroutine = null;
            }

            _smoothSliderCoroutine = StartCoroutine(ChangeSliderSmooth(currentHealth));
        }
        else
        {
            ChangeSlider(currentHealth);
        }
    }

    private void ChangeSlider(float health)
    {
        if (_health == null)
        {
            return;
        }

        _slider.value = health;
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