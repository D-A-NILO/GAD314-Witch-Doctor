using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{
    public float mouseSens;
    public Transform playerOrientation;

    InputAction lookAct;

    private float xRotation;
    private float yRotation;

    public Vector2 MouseDelta { get; private set; }
    public bool FreezeCam { get; private set; }

    [SerializeField] private InputAction freezeCamAction;

    
    void OnEnable()
    {
        freezeCamAction.Enable();
        freezeCamAction.performed += onCamFreeze;
        freezeCamAction.canceled += OnCamUnfreeze;
    }

    void OnDisable()
    {
        freezeCamAction.Disable();
        freezeCamAction.performed -= onCamFreeze;
        freezeCamAction.canceled -= OnCamUnfreeze;
    }

    private CinemachineCamera vCam;

    private void onCamFreeze(InputAction.CallbackContext cxt)
    {
        FreezeCam = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnCamUnfreeze(InputAction.CallbackContext cxt)
    {
        FreezeCam = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vCam = GetComponent<CinemachineCamera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        lookAct = InputSystem.actions.FindAction("Look");
        Vector3 rot = transform.rotation.eulerAngles;
        xRotation = rot.x;
        yRotation = rot.y;
    }

    // Update is called once per frame
    void Update()
    {

        if(FreezeCam) return;
        transform.position = playerOrientation.position;

        Vector2 lookValue = lookAct.ReadValue<Vector2>() * Time.deltaTime * mouseSens;
        MouseDelta = lookValue;



        yRotation += lookValue.x;
        xRotation -= lookValue.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        playerOrientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }

    public void SetSesitivity(float value)
    {
        mouseSens = value;
    }

    public void SetFOV(float value)
    {
        vCam.Lens.FieldOfView = value;
    }
}
