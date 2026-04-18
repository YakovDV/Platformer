using UnityEngine;

public class VampirismVisualizer : MonoBehaviour
{
    [SerializeField] private GameObject _visualEffect;
    [SerializeField] private VampirismAbility _vampirismAbility;

    private void Start()
    {
        _visualEffect.SetActive(false);
    }

    private void OnEnable()
    {
        _vampirismAbility.AbilityInProcess += OnAbilityInProcess;
    }

    private void OnDisable()
    {
        _vampirismAbility.AbilityInProcess -= OnAbilityInProcess;
    }

    private void OnAbilityInProcess(bool isInProcess)
    {
        _visualEffect.SetActive(isInProcess);
    }
}