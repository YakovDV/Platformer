using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Damager _damager;
    [SerializeField] private EnemyCharacterSensor _characterSensor;
    [SerializeField] private AttackAnimation _attackAnimation;
    [SerializeField] private float _attackDistance = 1f;
    [SerializeField] private float _delayBetweenAttacks = 1f;

    private float _delay = 0.5f;

    private Coroutine _attackCoroutine;

    public bool IsAttacking { get; private set; }

    private void Update()
    {
        Transform target = _characterSensor.TargetPosition;

        if (target == null || IsAttacking)
            return;

        Vector2 toTarget = (Vector2)target.position - (Vector2)transform.position;
        float attackDistanceSqr = _attackDistance * _attackDistance;

        if (toTarget.sqrMagnitude <= attackDistanceSqr)
        {
            _attackCoroutine = StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        IsAttacking = true;
        _attackAnimation.PlayAttackAnimation();

        yield return new WaitForSeconds(_delay);

        _damager.DamageDeal();

        yield return new WaitForSeconds(_delayBetweenAttacks);

        IsAttacking = false;
        _attackCoroutine = null;
    }
}