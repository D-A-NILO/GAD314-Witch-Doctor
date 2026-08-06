using UnityEngine;

public class NPCIllnessVisuals : MonoBehaviour
{
    public Renderer[] illnessRenderers;
    public Animator illnessAnimator;
    public IllnessParticleController particles;
    public void SetSeverity(float severity)
    {
        if(illnessRenderers.Length > 0)
            foreach (var renderer in illnessRenderers)
            {
                renderer.material.SetFloat("_Severity", severity);
            }

        if(illnessAnimator)
            illnessAnimator.SetFloat("severity", severity);

        if(particles)
            particles.SetSeverity(severity);
    }
}
