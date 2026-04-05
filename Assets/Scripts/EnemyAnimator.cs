using UnityEngine;

[RequireComponent(typeof(Animator))]

public class EnemyAnimator : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int IsDying = Animator.StringToHash("IsDying");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetSpeed(float speed)
    {
        _animator.SetFloat(Speed, speed, 0.1f, Time.deltaTime);
    }

    public void PlayDeathAnimation()
    {
        _animator.SetBool(IsDying, true);
    }
}