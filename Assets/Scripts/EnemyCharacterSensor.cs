using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class EnemyCharacterSensor : MonoBehaviour
{
    public Transform TargetPosition { get; private set; }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent<Character>(out Character character))
        {
            TargetPosition = character.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Character>(out Character character))
        {
            if (TargetPosition == character.transform)
            {
                TargetPosition = null;
            }
        }
    }
}