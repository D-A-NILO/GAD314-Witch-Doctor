using TMPro;
using UnityEngine;

public class InteractText : MonoBehaviour
{
    public GameObject text;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("entered trigger");

            text.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("exited trigger");

            text.SetActive(false);
        }
    }
}
