using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health), typeof(PlayerInput))]

public class VampirismAbility : MonoBehaviour
{
    [SerializeField] private float _abilityDuration = 6f;
    [SerializeField] private float _cooldownDuration = 4f;
    [SerializeField] private float _radius = 1.5f;
    [SerializeField] private int _damagePerSecond = 5;

    private Health _selfHealth;
    private PlayerInput _playerInput;

    private Coroutine _abilityCoroutine;
    private Coroutine _cooldown;

    private bool _canDrain = true;

    public Action<float> NormalizedStateChanged;
    public Action<bool> AbilityInProcess;

    public float AbilityStateSpeed {  get; private set; }

    private void Awake()
    {
        _selfHealth = GetComponent<Health>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        _playerInput.VampirismPressed += OnVampirismRequested;
    }

    private void OnDisable()
    {
        _playerInput.VampirismPressed -= OnVampirismRequested;
    }

    private void OnVampirismRequested()
    {
        Health target = SearchTarget();

        if (_canDrain == true)
        {
            if (_abilityCoroutine != null)
            {
                StopCoroutine(_abilityCoroutine);
                _abilityCoroutine = null;
            }

            AbilityInProcess?.Invoke(true);

            _abilityCoroutine = StartCoroutine(Drain(target));
        }
    }

    private Health SearchTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _radius);

        Health target = null;

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent<Health>(out Health health) && health != _selfHealth)
            {
                target = health;
                break;
            }
        }

        return target;
    }

    private IEnumerator Drain(Health health)
    {
        _canDrain = false;

        WaitForSeconds wait = new (1f);

        AbilityStateSpeed = 1 / _abilityDuration;

        for (float i = _abilityDuration; i >= 0f; i--)
        {
            if (health != null && health.IsDead == false)
            {
                health.TakeDamage(_damagePerSecond);
                _selfHealth.TakeHeal(_damagePerSecond);
            }

            NormalizedStateChanged?.Invoke(Mathf.Clamp01(i/_abilityDuration));

            yield return wait;
        }

        if (_cooldown != null)
        {
            StopCoroutine(_cooldown);
            _cooldown = null;
        }

        _cooldown = StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        WaitForSeconds wait = new (1f);

        AbilityStateSpeed = 1 / _cooldownDuration;

        AbilityInProcess?.Invoke(false);

        for (float i = 0; i <= _cooldownDuration; i++)
        {
            NormalizedStateChanged?.Invoke(Mathf.Clamp01(i/_cooldownDuration));

            yield return wait;
        }

        _canDrain = true;
    }
}