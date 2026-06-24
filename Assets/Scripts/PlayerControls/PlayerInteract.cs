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
        if (Physics.Raycast(r, out RaycastHit hitInfo, interactRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactable))
            {
                Debug.Log(hitInfo.collider.name);
                interactable.OnInteract();
            }
        }
    }

}
