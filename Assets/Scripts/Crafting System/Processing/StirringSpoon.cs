using UnityEngine;

public class StirringSpoon : MonoBehaviour
{
    public Camera playerCamera;
    public float stirSens = 0.2f;
    private Vector3 lastMousePos;
    private Cauldron currentCauldron;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            lastMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        { 
            Vector3 mouseDelta = Input.mousePosition - lastMousePos;
            lastMousePos = Input.mousePosition;

            float stirAmount = mouseDelta.magnitude * stirSens;

            if (currentCauldron != null)
            {
                currentCauldron.AddStir(stirAmount);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cauldron cauldron))
        { 
            currentCauldron = cauldron;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Cauldron cauldron))
        {
            if (currentCauldron == cauldron)
            {
                currentCauldron = null;
            }
        }
    }
}
