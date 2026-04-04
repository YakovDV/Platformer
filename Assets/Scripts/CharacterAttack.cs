using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private float _delay = 0.5f;
    [SerializeField] private AttackAnimation _attackAnimation;
    [SerializeField] private Damager _damager;

    private PlayerInput _playerInput;
    private Coroutine _attackCoroutine;

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
        _attackAnimation.PlayAttackAnimation();
        _damager.DamageDeal();

        yield return new WaitForSeconds(_delay);

        _isAttacking = false;
        _attackCoroutine = null;
    }
}