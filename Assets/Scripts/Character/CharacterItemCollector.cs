using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class CharacterItemCollector : MonoBehaviour
{
    [SerializeField] private PlayerWallet _wallet;
    [SerializeField] private Health _health;

    public void SetWallet(PlayerWallet wallet)
    {
        _wallet = wallet;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Coin>(out Coin coin))
        {
            _wallet.AddCoin();
            coin.PlaySound();
            Destroy(coin.gameObject);
        }
        else if (collision.gameObject.TryGetComponent<HealPack>(out HealPack healpack))
        {
            _health?.TakeHeal(healpack.HealValue);
            Destroy(healpack.gameObject);
        }
    }
}