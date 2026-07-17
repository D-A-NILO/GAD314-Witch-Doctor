using UnityEngine;

public class Grabbable : MonoBehaviour, IInteractable
{

    [SerializeField] private string displayName;

    public string DisplayName
    {
        get {return displayName;}
        set
        {
            displayName = value;
            //update textDisplay if being held
            if(holder != null) 
                ItemTextManager.SetText(displayName);
        }
    }
    public bool lockRotation = false;
    private Rigidbody rb;
    private PlayerInteract holder;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public virtual void OnInteract(PlayerInteract interactor) // on grab...
    {
        if(TryGetComponent(out Ingredient ingredient))
            ingredient.heldInTable?.RemoveHeldIngredient();
        holder = interactor;
        UnFreeze();

        interactor.GrabRigidBody(GetComponent<Rigidbody>(), this);
        ItemTextManager.SetText(displayName);
    }

    public void Freeze()
    {
        if(!rb) return;

        // rb.linearVelocity = Vector3.zero;
        // rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
    }

    public void UnFreeze()
    {
        rb.isKinematic = false;
    }

    public void OnDrop()
    {
        holder = null;
        ItemTextManager.SetText("");
    }

    public PlayerInteract GetHoldingInteractor()
    {
        return holder;
    }


}
