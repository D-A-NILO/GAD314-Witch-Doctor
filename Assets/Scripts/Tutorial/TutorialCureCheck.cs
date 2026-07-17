using UnityEngine;

public class TutorialCureCheck : MonoBehaviour
{
    public MonoTrigger OnCured;
    public MonoTrigger OnKilled;
    private NPCIllness illnessMan;

    void Awake()
    {
      illnessMan = GetComponent<NPCIllness>();  
    }

    bool triggered = false;
    void Update()
    {
        if(triggered) return;

        if(illnessMan.isCured)
        {
            OnCured.Trigger();
            triggered = true;
        }
        else
        {
            if(illnessMan.isDead)
            {
                OnKilled.Trigger();
                triggered = true;
            }   
        }
    }
}
