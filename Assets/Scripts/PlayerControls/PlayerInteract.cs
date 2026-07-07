using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{

    public Transform interactorSource;
    public float interactRange;
    public InputActionReference interactActionRef;

    [SerializeField] LayerMask includeLayers = int.MaxValue;
    public ItemHolder itemHolder;

    //private IInteractable[] interactables;
    
    private IInteractable visibleInteractible;
    private IInteractable holdingInteractible;
    private CrosshairIndicator indicator;

    private void OnEnable()
    {
        interactActionRef.action.Enable();
        interactActionRef.action.performed += OnInteractPressed;

        indicator = GetComponent<CrosshairIndicator>();

    }
    private void OnDisable()
    {
        interactActionRef.action.performed -= OnInteractPressed;
    }

    private void OnInteractPressed(InputAction.CallbackContext cxt)
    {
        Debug.Log("pressed");
        // if somthing to pickup
        if(holdingInteractible == null)
        {   //execute interactible
            visibleInteractible?.OnInteract(this);
        }else
        { // drop interactible
            holdingInteractible = null;
            itemHolder.ReleaseHeldRB();
        }
    }

    public void GrabRigidBody(Rigidbody rb, IInteractable interactable)
    {

        if(interactable is not Grabbable)
        {
            Debug.Log("interactible cannot be grabbed");
            return;
        }
        holdingInteractible = interactable;
        
        itemHolder.GrabRB(rb, (interactable as Grabbable).lockRotation);
    }

    void FixedUpdate()
    {
       

        IInteractable newInteractible;
        if(Physics.Raycast(interactorSource.position, interactorSource.forward, out RaycastHit hit, interactRange, includeLayers))
        {
            newInteractible = hit.collider.GetComponent<IInteractable>();
        }else 
            newInteractible = null;

        if(newInteractible != visibleInteractible) //if interactible changed
            OnInteractableUpdate(newInteractible);
              
    }

    private void OnInteractableUpdate(IInteractable newInteractible)
    {
        if(newInteractible == null)
            indicator.SetIndicatorStatus("None");
        else
            indicator.SetIndicatorStatus("Interact");
        

        visibleInteractible = newInteractible;  
    }

}
