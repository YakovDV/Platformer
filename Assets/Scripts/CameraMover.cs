using UnityEngine;

[RequireComponent(typeof(Camera))]

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Character _charecter;
    [SerializeField] private CharacterSpawner _respawn;
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;

    private Vector3 _offset = new(0f, 0f, -10f);

    private void OnEnable()
    {
        _respawn.CharacterSpawned += SetCharecter;
    }

    private void OnDisable()
    {
        _respawn.CharacterSpawned -= SetCharecter;
    }

    private void Update()
    {
        if (_charecter == null)
        {
            return;
        }

        Vector3 position = new(_charecter.transform.position.x, transform.position.y, 0f);
        position.x = Mathf.Clamp(position.x, _minX, _maxX);

        transform.position = position + _offset;
    }

    private void SetCharecter(Character character)
    {
        _charecter = character;
    }
}