using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 8f;

    private bool _isJumpRequested;

    public void Jump(Rigidbody2D rigidbody)
    {
        if (_isJumpRequested == true)
        {
            return;
        }

        _isJumpRequested = true;

        Vector2 jumpDirection = new(0, _jumpForce);
        rigidbody.AddForce(jumpDirection, ForceMode2D.Impulse);

        if (_isJumpRequested == false)
        {
            return;
        }

        _isJumpRequested = false;
    }
}