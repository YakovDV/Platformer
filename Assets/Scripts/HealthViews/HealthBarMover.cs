using UnityEngine;

public class HealthBarMover : MonoBehaviour
{
    [SerializeField] private GameObject _trackingTarget;
    [SerializeField] private HealthBarSmooth _healthBarSmooth;
    [SerializeField] private Vector3 _offset = new(0f, 2f, 0f);

    private void Update()
    {
        if (_trackingTarget == null)
        {
            return;
        }

        Vector3 position = new(_trackingTarget.transform.position.x, _trackingTarget.transform.position.y, 0f);

        transform.position = position + _offset;
    }

    public void SetTarget(GameObject target)
    {
        _trackingTarget = target;
    }
}