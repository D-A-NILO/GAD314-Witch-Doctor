using UnityEngine;
using UnityEngine.InputSystem.HID;

public class EquipItem : MonoBehaviour
{
    public Camera playerCamera;
    private float maxEquipDistance = 5;
    private GameObject currentItemHeld;
    private bool isHolding = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q") && isHolding == true)
        { 
            DropItem();
        }
    }

    public void Pickup()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, maxEquipDistance))
        {
            if (hit.transform.tag == "Pickupable")
            {

                if (isHolding) DropItem();

                currentItemHeld = hit.transform.gameObject;

                foreach (var c in hit.transform.GetComponentsInChildren<Collider>()) if (c != null) c.enabled = false;
                foreach (var r in hit.transform.GetComponentsInChildren<Rigidbody>()) if (r != null) r.isKinematic = true;

                currentItemHeld.transform.parent = transform;
                currentItemHeld.transform.localPosition = Vector3.zero;
                currentItemHeld.transform.localEulerAngles = Vector3.zero;

                isHolding = true;
            }
        }
    }

    public void DropItem()
    {
        currentItemHeld.transform.parent = null;
        foreach (var c in currentItemHeld.transform.GetComponentsInChildren<Collider>()) if (c != null) c.enabled = true;
        foreach (var r in currentItemHeld.transform.GetComponentsInChildren<Rigidbody>()) if (r != null) r.isKinematic = false;
        isHolding = false;
        RaycastHit hitDown;
        Physics.Raycast(transform.position, -Vector3.up, out hitDown);

        currentItemHeld.transform.position = hitDown.point;
    }
}
