using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private PlayerWallet _wallet;
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _wallet.AmountChanged += ShowBalance;
    }

    private void Start()
    {
        if (_wallet == null)
        {
            return;
        }

        ShowBalance(_wallet.Balance);
    }

    private void OnDisable()
    {
        _wallet.AmountChanged -= ShowBalance;
    }

    private void ShowBalance(int balance)
    {
        if (_wallet == null)
        {
            return;
        }

        _text.text = balance.ToString();
    }
}
