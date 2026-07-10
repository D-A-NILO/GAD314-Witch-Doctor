using TMPro;
using UnityEngine;

public class InteractText : MonoBehaviour
{
    public GameObject text;

    public void SetInteractText(GameObject interactText)
    {
        Debug.Log($"interaction text is set to: {interactText}");
        text = interactText;

        Debug.Log($"text assigned: {text}");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("entered trigger");
            if (text == null)
            {
                Debug.Log("inteaction text has no text assigned");
                return;
            }

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
