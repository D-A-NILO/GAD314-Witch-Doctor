using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemHoldDistancer : MonoBehaviour
{
    [SerializeField] private float holdDistanceMin = 1f;
    [SerializeField] private float holdDistanceMax = 2.5f;
    [SerializeField] private float defaultHoldDistance = 1.5f;
    private float holdDistance;
    [SerializeField] private Vector3 forwardAxis = Vector3.forward;

    [SerializeField] private float scrollSpeed = 0.2f;

    [SerializeField] private InputAction scrollAction;

    [SerializeField] private Transform holdTransform;

    void OnEnable()
    {
        scrollAction.performed += OnScroll;
        scrollAction.Enable();
    }

    void OnDisable()
    {
        scrollAction.performed -= OnScroll;
        scrollAction.Disable();        
    }

    void Start()
    {
      ResetDistance();  
    }

    public void ResetDistance()
    { 
        holdDistance = defaultHoldDistance;
        updateholdPos();
    }

    private void OnScroll(InputAction.CallbackContext context)
    {
        float delta = context.ReadValue<Vector2>().y;

        SetDistance(Mathf.Clamp(holdDistance + delta * scrollSpeed, holdDistanceMin, holdDistanceMax));
    }

    public void SetDistance(float distance)
    {
        holdDistance = distance;
        updateholdPos();
    }

    private void updateholdPos()
    {
        holdTransform.localPosition = forwardAxis * holdDistance;
    }
}
