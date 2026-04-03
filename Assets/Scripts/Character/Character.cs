using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class Character : MonoBehaviour
{
    private Collider2D _collider;
    private PlayerInput _playerInput;
    private bool _deathHandled = false;

    public event Action<bool> Dead;

    public bool IsDead { get; private set; }

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        if (IsDead == true && _deathHandled == false)
        {
            HandleDeath();
            _deathHandled = true;
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsDead = IsTouchingKiller(collision);
    }

    private bool IsTouchingKiller(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Killer>(out Killer killer))
        {
            return true;
        }

        return false;
    }

    private void HandleDeath()
    {
        Dead?.Invoke(true);
    }
}