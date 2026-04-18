using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string JumpButtonName = "Jump";
    private const string AttackButtonName = "Attack";
    private const string VampirismButtonName = "Vampirism";

    public Vector2 InputDirection { get; private set; }
    public event Action JumpPressed;
    public event Action AttackPressed;
    public event Action VampirismPressed;

    private void Update()
    {
        float xDirection = Input.GetAxis(Horizontal);

        if (Input.GetButtonDown(JumpButtonName))
        {
            JumpPressed?.Invoke();
        }

        if (Input.GetButtonDown(AttackButtonName))
        {
            AttackPressed?.Invoke();
        }

        if (Input.GetButtonDown(VampirismButtonName))
        {
            VampirismPressed?.Invoke();
        }

        InputDirection = new Vector2(xDirection, 0);
        InputDirection = Vector2.ClampMagnitude(InputDirection, 1f);
    }
}