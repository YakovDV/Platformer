using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Health), typeof(CharacterMover))]

public class CharacterAnimator : MonoBehaviour
{
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    private static readonly int IsDying = Animator.StringToHash("IsDying");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Attacking = Animator.StringToHash("Attacking");

    [SerializeField] private GroundSensor _groundSensor;
    [SerializeField] private CharacterAttack _characterAttack;

    private Animator _animator;
    private Health _health;
    private CharacterMover _characterMover;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
        _characterMover = GetComponent<CharacterMover>();
    }

    private void OnEnable()
    {
        _health.Died += PlayDeathAnimation;

        if (_characterAttack != null)
            _characterAttack.AttackStarted += OnAttackStarted;
    }

    private void OnDisable()
    {
        _health.Died -= PlayDeathAnimation;

        if (_characterAttack != null)
            _characterAttack.AttackStarted -= OnAttackStarted;
    }

    private void Update()
    {
        _animator.SetBool(IsJumping, _groundSensor.IsGrounded() == false);

        _animator.SetFloat(Speed, _characterMover.HorizontalSpeedNormalized, 0.1f, Time.deltaTime);
    }

    private void PlayDeathAnimation()
    {
        _animator.SetBool(IsDying, true);
    }

    private void OnAttackStarted()
    {
        _animator.SetTrigger(Attacking);
    }
}