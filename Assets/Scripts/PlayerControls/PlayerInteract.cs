using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UIElements;

public class PlayerInteract : MonoBehaviour
{

    public Transform interactorSource;
    public float interactRange;
    public InputActionReference interactActionRef;
    public InputActionReference openActionRef;

    [SerializeField] LayerMask includeLayers = int.MaxValue;
    public ItemHolder itemHolder;
    private ItemHoldDistancer distancer;

    //private IInteractable[] interactables;
    
    private IInteractable visibleInteractible;
    public IInteractable holdingInteractible;
    private CrosshairIndicator indicator;

    private void OnEnable()
    {
        interactActionRef.action.Enable();
        interactActionRef.action.performed += OnInteractPressed;

        openActionRef.action.Enable();
        openActionRef.action.performed += OnOpenPressed;

        indicator = GetComponent<CrosshairIndicator>();
        distancer = GetComponent<ItemHoldDistancer>();


    }
    private void OnDisable()
    {
        interactActionRef.action.performed -= OnInteractPressed;
        openActionRef.action.performed -= OnOpenPressed;
    }

    private void OnInteractPressed(InputAction.CallbackContext cxt)
    {
        // if somthing to pickup
        if(holdingInteractible == null)
        {   //execute interactible
            visibleInteractible?.OnInteract(this);
        }
        else if(holdingInteractible != null)
        { // drop interactible
            DropItem();
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
        distancer.SetDistance(Vector3.Distance(interactorSource.position, rb.position));
        indicator.SetIndicatorStatus("Holding");
    }

    public void DropItem()
    {
        (holdingInteractible as Grabbable).OnDrop();
        holdingInteractible = null;
        itemHolder.ReleaseHeldRB();
        indicator.SetIndicatorStatus("None");
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
        if(holdingInteractible == null)
        {
            if(newInteractible == null)
                indicator.SetIndicatorStatus("None");
            else
                indicator.SetIndicatorStatus("Interact");
        }
        

        visibleInteractible = newInteractible;  
    }

    private void OnOpenPressed(InputAction.CallbackContext cxt)
    {
        if (holdingInteractible is IngredientPackage package)
        {
            package.Open();
            holdingInteractible = null;
            itemHolder.ReleaseHeldRB();
            indicator.SetIndicatorStatus("None");
        }
    }

    public Grabbable GetHeldItem()
    {
        return holdingInteractible as Grabbable;
    }


}

