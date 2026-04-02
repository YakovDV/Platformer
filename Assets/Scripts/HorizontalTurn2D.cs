using UnityEngine;

public class HorizontalTurn2D : MonoBehaviour
{
    public void TurnToMovement(float value)
    {
        float rightAngle = 0f;
        float leftAngle = 180f;

        if (value > 0)
        {
            transform.localRotation = Quaternion.Euler(0f, rightAngle, 0f);
        }
        else if (value < 0)
        {
            transform.localRotation = Quaternion.Euler(0f, leftAngle, 0f);
        }
    }
}
