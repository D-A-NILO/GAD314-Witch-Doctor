using UnityEngine;

public abstract class IllnessParticleController : MonoBehaviour
{

    protected ParticleSystem particles;

    protected virtual void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public abstract void SetSeverity(float severity);
}
