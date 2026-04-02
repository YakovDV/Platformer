using System;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public event Action<int> AmountChanged;

    public int Balance { get; private set; }

    public void AddCoin(int count = 1)
    {
        Balance += count;
        AmountChanged?.Invoke(Balance);
    }
}