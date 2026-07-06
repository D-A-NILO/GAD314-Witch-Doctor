using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{

    public Transform interactorSource;
    public float interactRange;
    public InputActionReference interactActionRef;

    private void OnEnable()
    {
        interactActionRef.action.Enable();

        interactActionRef.action.performed += OnInteractPressed;

    }
    private void OnDisable()
    {
        interactActionRef.action.performed -= OnInteractPressed;
    }


    private void OnInteractPressed(InputAction.CallbackContext cxt)
    {
        Debug.Log("pressed");
        Ray r = new Ray(interactorSource.position, interactorSource.forward);
        Debug.DrawRay(interactorSource.position, interactorSource.forward * interactRange, Color.red, 2f);
        RaycastHit[] hits = Physics.RaycastAll(r, interactRange);

        foreach (RaycastHit hitInfo in hits)
        {
            if (hitInfo.collider.transform.IsChildOf(transform))
                continue;

            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactable))
            {
                interactable.OnInteract();
                break;
            }
        }
    }

}
