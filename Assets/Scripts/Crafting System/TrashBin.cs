using UnityEngine;

public class TrashBin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Grabbable grabbable))
            return;

        grabbable.GetHoldingInteractor()?.DropItem();
        Destroy(grabbable.gameObject);
    }
}
