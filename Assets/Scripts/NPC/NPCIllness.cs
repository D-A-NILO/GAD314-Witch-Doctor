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

    public Bottle bottle;
    public InteractText interactText;
    [SerializeField] private Dialogue dialogue;
    public NPCMovement npcMove;
    private NPCSpawner spawner;

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

    public void GivePotion(Bottle bottle)
    { 
        if(isDead || isCured)
            return;

        interactText.text.SetActive(false);

        if (bottle.CurrentPotionID == currentIllness.cureID)
        {
            illnessSeverity -= currentIllness.cureAmount;
                Debug.Log("correct potion");
        }
        else
        {
            illnessSeverity += currentIllness.worsenAmount;
                Debug.Log("wrong potion");
        }

        Debug.Log($"the severity of npc's Illness is: {illnessSeverity}");
        bottle.Empty();

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

        dialogue.onDialogueEnd += () =>
        {
            spawner.RemoveNPC(gameObject);
        };

        dialogue.StartDialogue(1);
    }

    private void KillPatient()
    {
        isDead = true;
        Debug.Log($"{gameObject.name} is dead");
        

        dialogue.onDialogueEnd = () =>
        {
            spawner.RemoveNPC(gameObject);
        };

        dialogue.StartDialogue(2);
    }

    public void SetDialogue(Dialogue dialogueRef)
    {
        dialogue = dialogueRef;
    }

    public void SetSpawner(NPCSpawner npcSpawner)
    {
        spawner = npcSpawner;
    }
}
