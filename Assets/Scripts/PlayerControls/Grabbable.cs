using UnityEngine;

public class Grabbable : MonoBehaviour, IInteractable
{
    public bool lockRotation = false;
    public void OnInteract(PlayerInteract interactor)
    {
        Debug.Log("bub");
        interactor.GrabRigidBody(GetComponent<Rigidbody>(), this);
    }

}
