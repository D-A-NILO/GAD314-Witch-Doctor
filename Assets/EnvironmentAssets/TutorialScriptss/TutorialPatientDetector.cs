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
    private bool isInitialized = false; // Prevents the false alarm!

    void Start()
    {
        illness = GetComponent<NPCIllness>();

        // FindObjectsInactive.Include forces Unity to find the Dialogue UI even if it is currently hidden!
        dialogueSystem = FindAnyObjectByType<Dialogue>(FindObjectsInactive.Include);
        tutorialManager = FindAnyObjectByType<TutorialMain>();

        // Wait 0.2 seconds for the NPCIllness to assign its random sickness before we start watching it
        Invoke(nameof(SetInitialSeverity), 0.2f);
    }

    void SetInitialSeverity()
    {
        lastSeverity = illness.illnessSeverity;
        isInitialized = true; // Now we are allowed to check for mistakes
    }

    void Update()
    {
        // Don't do anything until the baseline is set
        if (!isInitialized) return;

        // 1. Detect if the player started talking to THIS specific NPC
        if (!hasTalked && dialogueSystem != null && dialogueSystem.IsDialogueActive && dialogueSystem.illness == illness)
        {
            hasTalked = true;
            if (tutorialManager != null)
            {
                tutorialManager.CompleteStep(talkedToStep);
            }
        }

        // 2. Detect if the player gave the WRONG potion (Severity increased!)
        if (illness.illnessSeverity > lastSeverity)
        {
            if (tutorialManager != null)
            {
                tutorialManager.ShowTemporaryMessage(wrongPotionMessage);
            }
            lastSeverity = illness.illnessSeverity; // Reset to watch for the next mistake
        }
        else if (illness.illnessSeverity < lastSeverity)
        {
            lastSeverity = illness.illnessSeverity; // Update if they give a partially helpful potion
        }

        // 3. Detect the Cure
        if (illness.isCured)
        {
            if (tutorialManager != null)
            {
                tutorialManager.CompleteStep(curedStep);
            }
            enabled = false; // Turn off this detector
        }
    }
}
