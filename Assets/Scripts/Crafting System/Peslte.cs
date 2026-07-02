using UnityEngine;

public class Pestle : MonoBehaviour
{
    private float nextCuttingTime;

    public float cutCooldown = 0.25f;
    private void OnTriggerEnter(Collider other)
    {
        if (Time.time < nextCuttingTime)
        { 
            return;
        }

        CGrindArea grindArea = other.GetComponent<CGrindArea>();

        nextCuttingTime = Time.time + cutCooldown;

        if (grindArea != null)
        {
            grindArea.RegisterCut();
        }
    }
}
