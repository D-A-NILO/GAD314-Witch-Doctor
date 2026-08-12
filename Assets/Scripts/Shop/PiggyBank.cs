using UnityEngine;

public class PiggyBank : MonoBehaviour
{

    [SerializeField] private Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out CoinBag coinBag))
            return;

        PlayerInteract holder = coinBag.GetHoldingInteractor();
        holder?.DropItem();

        animator.SetTrigger("deposit");

        CurrencyManager.Instance?.Add(coinBag.Value);
        Destroy(coinBag.gameObject);
    }
}
