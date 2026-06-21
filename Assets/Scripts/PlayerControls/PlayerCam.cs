using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{
    public float mouseSens;
    public Transform playerOrientation;
    public Camera playerCamera;
    InputAction lookAct;

    private float xRotation;
    private float yRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        lookAct = InputSystem.actions.FindAction("Look");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerOrientation.position;

        Vector2 lookValue = lookAct.ReadValue<Vector2>() * Time.deltaTime * mouseSens; 

        yRotation += lookValue.x;
        xRotation -= lookValue.y;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        playerOrientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }
}
