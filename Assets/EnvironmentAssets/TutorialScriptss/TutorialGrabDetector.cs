using UnityEngine;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.Events;

public class TutorialGrabDetector : MonoBehaviour
{
    
    public Grabbable grabbableItem;

    
    public UnityEvent OnItemGrabbed;

    private bool wasHeld = false;

    void Update()
    {
        bool isHeld = grabbableItem.GetHoldingInteractor() != null;

        
        if (isHeld && !wasHeld)
        {
            OnItemGrabbed?.Invoke();
        }

        wasHeld = isHeld; // Store state for the next frame
    }
}
