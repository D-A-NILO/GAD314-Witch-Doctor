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
    [Tooltip("How far below the hold point this object's pivot hangs while held. Leave 0 for normal items; use ~1 for tall objects with their pivot at the bottom (e.g. NPCs) so they're held by the middle.")]
    public float holdHeightOffset = 0f;
    private Rigidbody rb;
    private PlayerInteract holder;

    protected virtual void Start()
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

    public virtual void OnDrop()
    {
        holder = null;
        ItemTextManager.SetText("");
    }

    public PlayerInteract GetHoldingInteractor()
    {
        return holder;
    }


}
