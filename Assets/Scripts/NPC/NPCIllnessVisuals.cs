using UnityEngine;

public class NPCIllnessVisuals : MonoBehaviour
{
    public Renderer[] illnessRenderers;
    public Animator illnessAnimator;
    public IllnessParticleController particles;
    public float smoothingDelta = 5f;
    public float smoothingTolerance = 0.05f;
    private float targetSeverity;
    private float currentSeverity;
    public void SetSeverity(float severity)
    {
        targetSeverity = severity;
    }

    void Update()
    {
        //ignore if wthin tolerance
        if(Mathf.Abs(currentSeverity - targetSeverity) <= smoothingTolerance) return;

        currentSeverity = Mathf.Lerp(currentSeverity, targetSeverity, smoothingDelta * Time.deltaTime);
        currentSeverity = Mathf.Clamp01(currentSeverity);

        if(illnessRenderers.Length > 0)
            foreach (var renderer in illnessRenderers)
            {
                renderer.material.SetFloat("_Severity", currentSeverity);
            }

        if(illnessAnimator)
            illnessAnimator.SetFloat("severity", currentSeverity);

        if(particles)
            particles.SetSeverity(currentSeverity);
    }
}
