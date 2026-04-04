using System;
using System.Collections;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private Character _prefab;
    [SerializeField] private PlayerWallet _wallet;
    [SerializeField] private HealthViewer _healthViewer;
    [SerializeField] private WaitForSeconds _delay = new(2f);

    private Character _character;
    private Health _health;
    private Coroutine _spawnDelay;

    public event Action<Character> CharacterSpawned;

    private void Start()
    {
        SpawnCharacter();
    }

    private void OnDisable()
    {
        UnsubscribeFromCharacter();
    }

    private void SpawnCharacterDelayed(bool isSpawnNeeded)
    {
        if (isSpawnNeeded == false)
            return;

        if (_spawnDelay != null)
            StopCoroutine(_spawnDelay);

        _spawnDelay = StartCoroutine(RespawnTimer());
    }

    private void SpawnCharacter()
    {
        _character = Instantiate(_prefab);

        if (_character.TryGetComponent<CharacterCoinCollector>(out CharacterCoinCollector collector))
        {
            collector.SetWallet(_wallet);
        }

        _health = _character.GetComponent<Health>();
        _healthViewer.SetHealth(_health);

        SubscribeToCharacter();

        CharacterSpawned?.Invoke(_character);
    }

    private IEnumerator RespawnTimer()
    {
        if (_character != null)
        {
            UnsubscribeFromCharacter();
        }

        yield return _delay;

        Destroy(_character.gameObject);

        SpawnCharacter();

        _spawnDelay = null;
    }

    private void SubscribeToCharacter()
    {
        if (_character != null)
        {
            _health.Died += SpawnCharacterDelayed;
        }
    }

    private void UnsubscribeFromCharacter()
    {
        if (_character != null)
        {
            _health.Died -= SpawnCharacterDelayed;
        }
    }
}