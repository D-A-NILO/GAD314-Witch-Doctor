using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NPCIllness : MonoBehaviour
{
    public IllnessData[] illnesses;

    public IllnessData currentIllness;

    [Range(0, 100)]
    public int illnessSeverity;

    public bool isDead;
    public bool isCured;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AssignRandomIllness();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignRandomIllness()
    {
        if (illnesses.Length == 0)
            return;

        currentIllness = illnesses[Random.Range(0, illnesses.Length)];

        illnessSeverity = currentIllness.startingSeverity;
    }

    public void GivePotion(Potion potion)
    { 
        if(isDead || isCured)
            return;

        if (potion.potionID == currentIllness.cureID)
        {
            illnessSeverity -= currentIllness.cureAmount;
        }
        else
        {
            illnessSeverity += currentIllness.worsenAmount;
        }

        illnessSeverity = Mathf.Clamp(illnessSeverity, 0, 100);

        CheckIllnessState();
    }

    private void CheckIllnessState()
    {
        if (illnessSeverity <= 0)
        {
            CurePatient();
        }
        else if (illnessSeverity >= 100)
        { 
            KillPatient();
        }
    }

    private void CurePatient()
    { 
        isCured =true;
        Debug.Log($"{gameObject.name} has been cured");
    }

    private void KillPatient()
    {
        isDead = true;
        Debug.Log($"{gameObject.name} is dead");
    }
}
