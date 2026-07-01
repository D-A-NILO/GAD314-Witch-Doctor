using UnityEngine;

public class Knife : MonoBehaviour
{
    private float nextCuttingTime;

    public float cutCooldown = 0.25f;
    private void OnTriggerEnter(Collider other)
    {
        if (Time.time < nextCuttingTime)
        { 
            return;
        }

        CutArea cutArea = other.GetComponent<CutArea>();

        nextCuttingTime = Time.time + cutCooldown;

        if (cutArea != null)
        {
            cutArea.RegisterCut();
        }
    }
}
