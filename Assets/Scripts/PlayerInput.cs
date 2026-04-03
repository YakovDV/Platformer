using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Jump = nameof(Jump);

    public Vector2 InputDirection { get; private set; }
    public event Action JumpPressed;

    private void Update()
    {
        float xDirection = Input.GetAxis(Horizontal);

        if (Input.GetButtonDown(Jump))
        {
            JumpPressed?.Invoke();
        }

        InputDirection = new Vector2(xDirection, 0);
        InputDirection = Vector2.ClampMagnitude(InputDirection, 1f);
    }
}