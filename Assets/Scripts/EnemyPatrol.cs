using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private WaitForSeconds _waitOnPointTime = new(1f);

    private Coroutine _waitOnPointCoroutine;
    private int _currentPointIndex = 0;

    public bool IsWaiting { get; private set; }

    public Transform CurrentPoint
    {
        get
        {
            if (_points == null || _points.Length == 0)
            {
                return null;
            }

            return _points[_currentPointIndex];
        }
    }

    private void FixedUpdate()
    {
        if (_points == null || _points.Length == 0)
            return;

        if (IsWaiting)
            return;

        if (IsTargetReached())
        {
            if (_waitOnPointCoroutine == null)
            {
                _waitOnPointCoroutine = StartCoroutine(WaitOnPoint());
            }
        }
    }

    private IEnumerator WaitOnPoint()
    {
        IsWaiting = true;

        yield return _waitOnPointTime;

        SetNextPoint();

        IsWaiting = false;
        _waitOnPointCoroutine = null;
    }

    private bool IsTargetReached()
    {
        float distance = 1f;
        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = _points[_currentPointIndex].transform.position;

        return (targetPosition - currentPosition).sqrMagnitude <= distance * distance;
    }

    private void SetNextPoint()
    {
        _currentPointIndex++;

        if (_currentPointIndex >= _points.Length)
        {
            _currentPointIndex = 0;
        }
    }
}