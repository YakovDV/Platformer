using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class VampirismBar : MonoBehaviour
{
    [SerializeField] private VampirismAbility _vampirismAbility;

    private Coroutine _smoothSliderCoroutine;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        if (_vampirismAbility == null)
        {
            return;
        }

        SetAbility(_vampirismAbility);
    }

    private void OnDisable()
    {
        _vampirismAbility.NormalizedStateChanged -= ShowState;
    }

    public void SetAbility(VampirismAbility vampirismAbility)
    {
        if (_slider.gameObject.activeSelf == false)
        {
            _slider.gameObject.SetActive(true);
        }

        _vampirismAbility = vampirismAbility;

        _slider.maxValue = 1f;
        _slider.minValue = 0f;

        _vampirismAbility.NormalizedStateChanged += ShowState;
        ShowState(_slider.maxValue);
    }

    private void ShowState(float state)
    {
        if (_smoothSliderCoroutine != null)
        {
            StopCoroutine(_smoothSliderCoroutine);
            _smoothSliderCoroutine = null;
        }

        _smoothSliderCoroutine = StartCoroutine(ChangeSliderSmooth(state));
    }

    private IEnumerator ChangeSliderSmooth(float state)
    {
        while (Mathf.Approximately(_slider.value, state) == false)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, state, _vampirismAbility.AbilityStateSpeed * Time.deltaTime);

            yield return null;
        }

        _slider.value = state;
    }
}