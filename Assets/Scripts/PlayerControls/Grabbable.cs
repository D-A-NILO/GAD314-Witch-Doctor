using UnityEngine;

public class Grabbable : MonoBehaviour, IInteractable
{
    public bool lockRotation = false;
    private Rigidbody rb;
    private PlayerInteract holder;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void OnInteract(PlayerInteract interactor)
    {
        if(TryGetComponent(out Ingredient ingredient))
            ingredient.heldInTable?.RemoveHeldIngredient();
        holder = interactor;
        UnFreeze();

        interactor.GrabRigidBody(GetComponent<Rigidbody>(), this);
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
    }

    public PlayerInteract GetHoldingInteractor()
    {
        return holder;
    }

}
