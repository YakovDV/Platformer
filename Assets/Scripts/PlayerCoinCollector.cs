using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class PlayerCoinCollector : MonoBehaviour
{
    [SerializeField] private PlayerWallet _wallet;
    private Collider2D _collider;

    public void SetWallet(PlayerWallet wallet)
    {
        _wallet = wallet;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Coin>(out Coin coin))
        {
            _wallet?.AddCoin();
            coin.PlaySound();
            Destroy(coin.gameObject);
        }
    }
}