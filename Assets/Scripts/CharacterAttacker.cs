using System.Collections;
using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]

public class CharacterAttacker : MonoBehaviour
{
    [SerializeField] private float _delay = 0.5f;
    [SerializeField] private Damager _damager;

    private PlayerInput _playerInput;
    private Coroutine _attackCoroutine;

    public event Action AttackStarted;

    private bool _isAttacking;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        _playerInput.AttackPressed += OnAttackPressed;
    }

    private void OnDisable()
    {
        _playerInput.AttackPressed -= OnAttackPressed;
    }

    private void OnAttackPressed()
    {
        if (_damager == null)
        {
            return;
        }

        if (_isAttacking)
        {
            return;
        }

        _attackCoroutine = StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        _isAttacking = true;
        AttackStarted?.Invoke();
        _damager.DamageDeal();

        yield return new WaitForSeconds(_delay);

        _isAttacking = false;
        _attackCoroutine = null;
    }
}