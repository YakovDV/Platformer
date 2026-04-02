using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private KeyCode _jumpKey = KeyCode.UpArrow;

    public Vector2 InputDirection { get; private set; }
    public bool IsJumpRequested { get; private set; }

    private void Update()
    {
        ReadInput();
    }

    public void ResetJumpRequest()
    {
        if (IsJumpRequested == false)
        {
            return;
        }

        IsJumpRequested = false;
    }

    private void ReadInput()
    {
        float xDirection = Input.GetAxis(Horizontal);

        if (Input.GetKeyDown(_jumpKey))
        {
            IsJumpRequested = true;
        }

        InputDirection = new Vector2(xDirection, 0);
        InputDirection = Vector2.ClampMagnitude(InputDirection, 1f);
    }
}