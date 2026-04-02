using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Character), typeof(CharacterMover))]

public class CharacterAnimator : MonoBehaviour
{
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    private static readonly int IsDying = Animator.StringToHash("IsDying");
    private static readonly int Speed = Animator.StringToHash("Speed");

    private Animator _animator;
    private Character _character;
    private CharacterMover _characterMover;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _character = GetComponent<Character>();
        _characterMover = GetComponent<CharacterMover>();
    }

    private void OnEnable()
    {
        _character.ImDead += PlayDeathAnimation;
    }

    private void OnDisable()
    {
        _character.ImDead -= PlayDeathAnimation;
    }

    private void Update()
    {
        _animator.SetBool(IsJumping, !_character.IsGrounded);

        _animator.SetFloat(Speed, _characterMover.NormalizedHorizontalSpeed, 0.1f, Time.deltaTime);
    }

    private void PlayDeathAnimation(bool isDead)
    {
        _animator.SetBool(IsDying, isDead);
    }
}