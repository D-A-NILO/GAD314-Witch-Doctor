using UnityEngine;

public class StirringSpoon : MonoBehaviour
{
    public float stirSens = 0.2f;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other) 
    {

        Cauldron currentCauldron = other.GetComponent<Cauldron>();

        // if (Input.GetMouseButtonDown(0))
        // { 
        //     lastMousePos = Input.mousePosition;
        // }

        
        // Vector3 mouseDelta = Input.mousePosition - lastMousePos;
        // lastMousePos = Input.mousePosition;

        float stirAmount = rb.linearVelocity.magnitude * stirSens;

        if (currentCauldron != null)
        {
            currentCauldron.AddStir(stirAmount);
            Debug.Log("Mixxing cauldron: " + stirAmount);
        }
    }

}
