using UnityEngine;

[RequireComponent(typeof(Animator))]

public class AttackAnimation : MonoBehaviour
{
    private static readonly int Attacking = Animator.StringToHash("Attacking");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger(Attacking);
    }
}