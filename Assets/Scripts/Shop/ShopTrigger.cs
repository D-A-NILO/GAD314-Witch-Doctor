using UnityEngine;

public class ShopTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private ShopUI shopUI;

    public virtual void OnInteract(PlayerInteract interactor)
    {
        PlayerController controller = interactor.GetComponent<PlayerController>();
        if (controller == null)
        {
            Debug.LogWarning("ShopTrigger: no PlayerController found on interactor");
            return;
        }

        controller.SetControl(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        shopUI.Open(controller);
    }
}