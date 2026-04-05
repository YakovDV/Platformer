using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Health), typeof(CharacterMover))]

public class CharacterAnimator : MonoBehaviour
{
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    private static readonly int IsDying = Animator.StringToHash("IsDying");
    private static readonly int Speed = Animator.StringToHash("Speed");

    [SerializeField] private GroundSensor _groundSensor;

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
    }

    private void OnDisable()
    {
        _health.Died -= PlayDeathAnimation;
    }

    private void Update()
    {
        _animator.SetBool(IsJumping, !_groundSensor.IsGrounded());

        _animator.SetFloat(Speed, _characterMover.HorizontalSpeedNormalized, 0.1f, Time.deltaTime);
    }

    private void PlayDeathAnimation()
    {
        _animator.SetBool(IsDying, true);
    }
}