using UnityEngine;

public class NPCIllnessVisuals : MonoBehaviour
{
    public Renderer[] illnessRenderers;
    public void SetSeverity(float severity)
    {
        foreach (var renderer in illnessRenderers)
        {
            renderer.material.SetFloat("_Severity", severity);
        }
    }
}
