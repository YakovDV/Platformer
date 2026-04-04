using UnityEngine;

[RequireComponent(typeof(Animator), typeof(EnemyMover), typeof(Health))]

public class EnemyAnimator : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int IsDying = Animator.StringToHash("IsDying");

    private Animator _animator;
    private EnemyMover _enemyMover;
    private Health _health;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
        _enemyMover = GetComponent<EnemyMover>();
    }

    private void OnEnable()
    {
        _health.Died += PlayDeathAnimation;
    }

    private void OnDisable()
    {
        _health.Died -= PlayDeathAnimation;
    }

    private void Update()
    {
        _animator.SetFloat(Speed, _enemyMover.NormalizedHorizontalSpeed(), 0.1f, Time.deltaTime);
    }

    private void PlayDeathAnimation(bool isDead)
    {
        _animator.SetBool(IsDying, isDead);
    }
}