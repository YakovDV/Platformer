using UnityEngine;

[RequireComponent(typeof(Animator))]

public class EnemyAnimator : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int IsDying = Animator.StringToHash("IsDying");
    private static readonly int Attacking = Animator.StringToHash("Attacking");

    [SerializeField] private EnemyAttack _enemyAttack;
    [SerializeField] private EnemyMover _enemyMover;
    [SerializeField] private EnemyDeath _enemyDeath;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _enemyAttack.AttackStarted += OnAttackStarted;
        _enemyMover.SpeedChanged += OnSpeedChaged;
        _enemyDeath.Died += OnDied;
    }

    private void OnAttackStarted()
    {
        _animator.SetTrigger(Attacking);
    }

    private void OnSpeedChaged(float speed)
    {
        _animator.SetFloat(Speed, speed, 0.1f, Time.deltaTime);
    }

    private void OnDied(bool isDied)
    {
        _animator.SetBool(IsDying, isDied);
    }
}