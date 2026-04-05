using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    [SerializeField] private LayerMask _ground;
    [SerializeField] private Vector2 _boxSize = new(0.5f, 0.1f);

    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(transform.position, _boxSize, 0f, _ground);
    }
}