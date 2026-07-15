using System;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    [SerializeField] private int startingBalance = 10000;

    public int Balance { get; private set; }
    public event Action<int> OnBalanceChanged;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Balance = startingBalance;
    }

    public bool Spend(int amount)
    {
        if (amount < 0 || amount > Balance)
            return false;

        Balance -= amount;
        OnBalanceChanged?.Invoke(Balance);
        return true;
    }

    public void Add(int amount)
    {
        if (amount <= 0) return;

        Balance += amount;
        OnBalanceChanged?.Invoke(Balance);
    }
}
