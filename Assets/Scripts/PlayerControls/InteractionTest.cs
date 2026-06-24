using UnityEngine;

public class InteractionTest : MonoBehaviour, IInteractable
{
    public EquipItem equipItem;
    public void OnInteract()
    {
        Debug.Log("was interacted with");
        equipItem.Pickup();
    }
}
