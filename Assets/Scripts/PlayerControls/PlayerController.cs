using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerInteract interactor;
    private ItemHoldDistancer itemDistancer;
    private PlayerCam cam;

    void Awake()
    {
      movement = GetComponent<PlayerMovement>();
      interactor = GetComponent<PlayerInteract>();
      itemDistancer = GetComponent<ItemHoldDistancer>();
      cam = GetComponentInChildren<PlayerCam>();
    }

    public void SetControl(bool enabled)
    {
        movement.enabled = enabled;
        interactor.enabled = enabled;
        itemDistancer.enabled = enabled;
        cam.enabled = enabled;
    }

}
