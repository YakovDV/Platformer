using System;
using System.Collections;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private Character _prefab;
    [SerializeField] private PlayerWallet _wallet;
    [SerializeField] private HealthViewer _healthViewer;
    [SerializeField] private WaitForSeconds _delay = new(2f);
    [SerializeField] private HealthBarSmooth _healthBarSmooth;
    [SerializeField] private HealthBarMover _healthBarMover;

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

    private void SpawnCharacterDelayed()
    {
        if (_spawnDelay != null)
            StopCoroutine(_spawnDelay);

        _spawnDelay = StartCoroutine(RespawnTimer());
    }

    private void SpawnCharacter()
    {
        _character = Instantiate(_prefab);

        if (_character.TryGetComponent<CharacterItemCollector>(out CharacterItemCollector collector))
        {
            collector.SetWallet(_wallet);
        }

        if (_character.TryGetComponent<Health>(out Health health))
        {
            _health = health;
        }

        _healthViewer.SetHealth(_health);

        if (_healthBarSmooth != null)
        {
            _healthBarSmooth.SetHealth(_health);
        }

        if (_healthBarMover != null)
        {
            _healthBarMover.SetTarget(_character.gameObject);
        }

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