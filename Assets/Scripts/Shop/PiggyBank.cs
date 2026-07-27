using UnityEngine;

public class PiggyBank : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out CoinBag coinBag))
            return;

        PlayerInteract holder = coinBag.GetHoldingInteractor();
        holder?.DropItem();

        CurrencyManager.Instance?.Add(coinBag.Value);
        Destroy(coinBag.gameObject);
    }
}
