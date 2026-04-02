using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class Character : MonoBehaviour
{
    [SerializeField] private LayerMask _ground;
    [SerializeField] private LayerMask _killers;
    [SerializeField] private float _groundDetectionFrequency = 0.1f;

    private Collider2D _collider;
    private bool _isGrounded;
    private bool _isDead = false;
    private bool _deathHandled = false;

    private Coroutine _groundDetectionCoroutine;

    public event Action<bool> Dead;

    public bool IsGrounded => _isGrounded;
    public bool IsDead => _isDead;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        _groundDetectionCoroutine = StartCoroutine(UpdateGroundedState());
    }

    private void OnDisable()
    {
        if (_groundDetectionCoroutine != null)
        {
            StopCoroutine(_groundDetectionCoroutine);
        }
    }

    private void FixedUpdate()
    {
        if (_isDead == true && _deathHandled == false)
        {
            HandleDeath();
            _deathHandled = true;
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isDead = IsTouchingKiller(collision);
    }

    private IEnumerator UpdateGroundedState()
    {
        WaitForSeconds detectionFrequency = new(_groundDetectionFrequency);

        while (_isDead == false)
        {
            Vector2 boxCastOrigin = new(_collider.bounds.center.x, _collider.bounds.min.y);
            Vector2 boxCastDirection = new(0, -1);
            Vector2 boxCastSize = new(_collider.bounds.size.x, _collider.bounds.size.y * 0.1f);

            _isGrounded = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0, boxCastDirection, 0, _ground);

            yield return detectionFrequency;
        }
    }

    private bool IsTouchingKiller(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & _killers.value) != 0)
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