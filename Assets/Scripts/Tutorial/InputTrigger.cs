using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTrigger : MonoTrigger
{
    public InputAction inputAction;



    void OnEnable()
    {
        inputAction.Enable();
        inputAction.performed += OnInput;
    }

    void OnDisable()
    {
        inputAction.performed -= OnInput;
    }

    private void OnInput(InputAction.CallbackContext context)
    {
        Trigger();
    }
}
