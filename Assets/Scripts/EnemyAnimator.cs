using UnityEngine;

[RequireComponent(typeof(Animator), typeof(EnemyMover))]

public class EnemyAnimator : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");

    private Animator _animator;
    private EnemyMover _enemyMover;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyMover = GetComponent<EnemyMover>();
    }

    private void Update()
    {
        _animator.SetFloat(Speed, _enemyMover.NormalizedHorizontalSpeed, 0.1f, Time.deltaTime);
    }
}