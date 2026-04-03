using UnityEngine;

public class LevelRestart : MonoBehaviour
{
    [SerializeField] private CharacterSpawner _characterSpawner;
    [SerializeField] private CameraMover _cameraMover;

    private void OnEnable()
    {
        _characterSpawner.CharacterSpawned += OnCharacterSpawned;
    }

    private void OnDisable()
    {
        _characterSpawner.CharacterSpawned -= OnCharacterSpawned;
    }

    private void OnCharacterSpawned(Character character)
    {
        _cameraMover.SetCharacter(character);
    }
}