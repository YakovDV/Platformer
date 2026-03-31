using System;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    private int _balance = 0;

    public event Action<int> AmountChanged;

    public int Balance => _balance;

    public void AddCoin(int count = 1)
    {
        _balance += count;
        AmountChanged?.Invoke(_balance);
    }
}