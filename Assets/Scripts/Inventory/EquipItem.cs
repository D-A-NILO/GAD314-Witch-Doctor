using UnityEngine;
using UnityEngine.InputSystem.HID;

public class EquipItem : MonoBehaviour
{
    public Camera playerCamera;
    private float maxEquipDistance = 5;

    private Item[] hotbar = new Item[5]; // 5 slots
    public int activeSlot = 0;

    void Awake()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    void Update()
    {
        // Switch slots with number keys
        if (Input.GetKeyDown("1")) SetActiveSlot(0);
        if (Input.GetKeyDown("2")) SetActiveSlot(1);
        if (Input.GetKeyDown("3")) SetActiveSlot(2);
        if (Input.GetKeyDown("4")) SetActiveSlot(3);
        if (Input.GetKeyDown("5")) SetActiveSlot(4);

        // Scroll wheel to cycle slots
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) SetActiveSlot((activeSlot + 1) % hotbar.Length);
        if (scroll < 0f) SetActiveSlot((activeSlot - 1 + hotbar.Length) % hotbar.Length);

        // Drop active item
        if (Input.GetKeyDown("q") && hotbar[activeSlot] != null)
            DropItem(activeSlot);
    }

    public void Pickup()
    {
        RaycastHit hit;
        if (!Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, maxEquipDistance)) return;
        if (hit.transform.tag != "Pickupable") return;

        // Find first empty slot
        int emptySlot = -1;
        for (int i = 0; i < hotbar.Length; i++)
        {
            if (hotbar[i] == null) { emptySlot = i; break; }
        }

        if (emptySlot == -1) { Debug.Log("Hotbar full!"); return; }

        GameObject item = hit.transform.gameObject;

        foreach (var c in item.GetComponentsInChildren<Collider>()) if (c != null) c.enabled = false;
        foreach (var r in item.GetComponentsInChildren<Rigidbody>()) if (r != null) r.isKinematic = true;

        hotbar[emptySlot] = new Item(item);

        // Hide it if not the active slot
        if (emptySlot != activeSlot)
            item.SetActive(false);
        else
            AttachToCamera(item);
    }

    void SetActiveSlot(int slot)
    {
        // Hide current
        if (hotbar[activeSlot] != null)
            hotbar[activeSlot].itemObject.SetActive(false);

        activeSlot = slot;

        // Show new
        if (hotbar[activeSlot] != null)
        {
            hotbar[activeSlot].itemObject.SetActive(true);
            AttachToCamera(hotbar[activeSlot].itemObject);
        }

        Debug.Log($"Active slot: {activeSlot + 1}");
    }

    void AttachToCamera(GameObject item)
    {
        item.transform.parent = transform;
        item.transform.localPosition = Vector3.zero;
        item.transform.localEulerAngles = Vector3.zero;
    }

    public void DropItem(int slot)
    {
        if (hotbar[slot] == null) return;

        GameObject item = hotbar[slot].itemObject;
        item.transform.parent = null;
        item.SetActive(true);

        foreach (var c in item.GetComponentsInChildren<Collider>()) if (c != null) c.enabled = true;
        foreach (var r in item.GetComponentsInChildren<Rigidbody>()) if (r != null) r.isKinematic = false;

        RaycastHit hitDown;
        Physics.Raycast(transform.position, -Vector3.up, out hitDown);
        if (hitDown.collider != null)
            item.transform.position = hitDown.point;

        hotbar[slot] = null;
    }
}
