using System;
using System.Collections;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private Character _prefab;
    [SerializeField] private PlayerWallet _wallet;
    [SerializeField] private WaitForSeconds _delay = new(2f);

    private Coroutine _spawnDelay;

    public event Action<Character> CharacterSpawned;

    private void OnEnable()
    {
        SubscribeToCharacter();
    }

    private void OnDisable()
    {
        UnsubscribeFromCharacter();
    }

    private void SpawnCharacter(bool isSpawnNeeded)
    {
        if (isSpawnNeeded == false)
            return;

        if (_spawnDelay != null)
            StopCoroutine(_spawnDelay);

        _spawnDelay = StartCoroutine(RespawnTimer());
    }

    private IEnumerator RespawnTimer()
    {
        if (_character != null)
        {
            UnsubscribeFromCharacter();
        }

        yield return _delay;

        Destroy(_character.gameObject);

        _character = Instantiate(_prefab);

        if (_character.TryGetComponent<CharacterCoinCollector>(out CharacterCoinCollector collector))
        {
            collector.SetWallet(_wallet);
        }

        SubscribeToCharacter();

        CharacterSpawned?.Invoke(_character);

        _spawnDelay = null;
    }

    private void SubscribeToCharacter()
    {
        if (_character != null)
        {
            _character.Dead += SpawnCharacter;
        }
    }

    private void UnsubscribeFromCharacter()
    {
        if (_character != null)
        {
            _character.Dead -= SpawnCharacter;
        }
    }
}