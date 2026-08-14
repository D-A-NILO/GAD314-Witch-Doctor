using UnityEngine;

public class TutorialPatientDetector : MonoBehaviour
{
    [Header("Tutorial Step Settings")]
    public int talkedToStep = 14;
    public int curedStep = 15;

    [Header("Failure Settings")]
    [TextArea(2, 3)]
    public string wrongPotionMessage = "Wrong potion! Their symptoms just got worse. Check the recipe and try again.";

    private Dialogue dialogueSystem;
    private NPCIllness illness;
    private TutorialMain tutorialManager;

    
    private float lastSeverity;
    private bool hasTalked = false;
    private bool isInitialized = false;

    void Start()
    {
        illness = GetComponent<NPCIllness>();
        dialogueSystem = FindAnyObjectByType<Dialogue>(FindObjectsInactive.Include);
        tutorialManager = FindAnyObjectByType<TutorialMain>();
    }

    void Update()
    {
        
        if (!isInitialized)
        {
            if (illness.illnessSeverity > 0f)
            {
                lastSeverity = illness.illnessSeverity;
                isInitialized = true;
            }
            return;
        }

        
        if (!hasTalked && dialogueSystem != null && dialogueSystem.IsDialogueActive && dialogueSystem.illness == illness)
        {
            hasTalked = true;
            if (tutorialManager != null)
                tutorialManager.CompleteStep(talkedToStep);
        }

        
        if (illness.illnessSeverity > lastSeverity + 0.1f)
        {
            if (tutorialManager != null)
                tutorialManager.ShowTemporaryMessage(wrongPotionMessage);

            lastSeverity = illness.illnessSeverity;
        }
        else if (illness.illnessSeverity != lastSeverity)
        {
           
            lastSeverity = illness.illnessSeverity;
        }

        
        if (illness.isCured)
        {
            if (tutorialManager != null)
                tutorialManager.CompleteStep(curedStep);

            enabled = false;
        }
        
        else if (illness.isDead)
        {
            if (tutorialManager != null)
            {
                tutorialManager.ShowTemporaryMessage("The patient died! Please wait for a new patient to arrive.");
                tutorialManager.PlayStep(talkedToStep);
            }
            enabled = false;
        }
    }
}