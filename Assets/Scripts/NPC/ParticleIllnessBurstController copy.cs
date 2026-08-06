using UnityEngine;

public class ParticleIllnessBurstController : IllnessParticleController
{

    public float ProbabilityMin, ProbabilityMax;
    public int burstIndex = 0;


    protected override void Start()
    {
        base.Start();

        ParticleSystem.Burst burst = particles.emission.GetBurst(burstIndex);
        
        burst.probability = 0;

        particles.emission.SetBurst(burstIndex, burst);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void SetSeverity(float severity)
    {
        if(particles == null || particles.emission.burstCount <= 0)
            return;
        
        ParticleSystem.Burst burst = particles.emission.GetBurst(burstIndex);
        
        //remap to MinMax
        burst.probability = ProbabilityMin + (ProbabilityMax - ProbabilityMin) * severity;

        particles.emission.SetBurst(burstIndex, burst);
    }
}
