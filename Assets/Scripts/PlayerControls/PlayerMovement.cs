using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    InputAction moveAct;

    Rigidbody rb; 

    public float moveSpeed = 10;
    private float xMove;
    private float yMove;

    public Transform playerOrientation;
    public Camera playerCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveAct = InputSystem.actions.FindAction("Move");
    }


    private void FixedUpdate()
    {
        Vector2 moveValue = moveAct.ReadValue<Vector2>();
        xMove = moveValue.x;
        yMove = moveValue.y;

        Vector3 moveDirection = playerOrientation.forward * moveValue.y + playerOrientation.right * moveValue.x;

        rb.AddForce(moveDirection * moveSpeed, ForceMode.Force);
    }
}
