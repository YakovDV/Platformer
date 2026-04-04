using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Jump = nameof(Jump);
    private const string Attack = nameof(Attack);

    public Vector2 InputDirection { get; private set; }
    public event Action JumpPressed;
    public event Action AttackPressed;

    private void Update()
    {
        float xDirection = Input.GetAxis(Horizontal);

        if (Input.GetButtonDown(Jump))
        {
            JumpPressed?.Invoke();
        }

        if (Input.GetButtonDown(Attack))
        {
            AttackPressed?.Invoke();
        }

        InputDirection = new Vector2(xDirection, 0);
        InputDirection = Vector2.ClampMagnitude(InputDirection, 1f);
    }
}