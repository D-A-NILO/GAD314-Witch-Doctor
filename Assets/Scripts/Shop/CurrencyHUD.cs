using TMPro;
using UnityEngine;

public class CurrencyHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text balanceText;

    void Start()
    {
        Subscribe();
    }

    void OnEnable()
    {
        Subscribe();
    }

    void OnDisable()
    {
        if (CurrencyManager.Instance != null)
            CurrencyManager.Instance.OnBalanceChanged -= OnBalanceChanged;
    }

    private void Subscribe()
    {
        if (CurrencyManager.Instance == null)
            return;

        CurrencyManager.Instance.OnBalanceChanged -= OnBalanceChanged;
        CurrencyManager.Instance.OnBalanceChanged += OnBalanceChanged;
        balanceText.text = $"Coin: {CurrencyManager.Instance.Balance}c";
    }

    private void OnBalanceChanged(int newBalance)
    {
        balanceText.text = $"Coin: {newBalance}c";
    }
}
