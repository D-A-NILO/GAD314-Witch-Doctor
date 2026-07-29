using UnityEngine;

public class CoinBag : Grabbable
{
    [SerializeField] private int value = 10;

    public int Value => value;

    public void SetValue(int amount)
    {
        value = amount;
    }
}
