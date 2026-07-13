using System.Collections;
using UnityEngine;
using TMPro;

public enum TutorialState
{
    GoToSpawnTable,
    GoToChoppingBoard,
    GoToCauldron,
    Complete
}

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    [Header("UI Reference")]
    public TextMeshProUGUI tutorialText;

    [Header("Typewriter Settings")]
    public float typingSpeed = 0.03f; 

    [Header("Visual Markers")]
    public GameObject spawnTableMarker;
    public GameObject choppingBoardMarker;
    public GameObject cauldronMarker;

    public TutorialState currentState = TutorialState.GoToSpawnTable;

   
    private Coroutine typingCoroutine;
    private string currentTargetText = "";
    private bool isTyping = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateTutorialProgress();
    }

    void Update()
    {
        // If the text is currently typing, allow the player to skip it by pressing Space or Left Click
        if (isTyping && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            SkipTyping();
        }
    }

    public void OnPlayerStepOnMarker(TutorialState stepOnState)
    {
       
        if (currentState == stepOnState)
        {
            if (currentState == TutorialState.GoToSpawnTable) currentState = TutorialState.GoToChoppingBoard;
            else if (currentState == TutorialState.GoToChoppingBoard) currentState = TutorialState.GoToCauldron;
            else if (currentState == TutorialState.GoToCauldron) currentState = TutorialState.Complete;

            UpdateTutorialProgress();
        }
    }

    void UpdateTutorialProgress()
    {
        
        if (spawnTableMarker) spawnTableMarker.SetActive(false);
        if (choppingBoardMarker) choppingBoardMarker.SetActive(false);
        if (cauldronMarker) cauldronMarker.SetActive(false);

        
        switch (currentState)
        {
            case TutorialState.GoToSpawnTable:
                if (spawnTableMarker) spawnTableMarker.SetActive(true);
                StartTypewriter("Welcome to the clinic! Please proceed to the table and press 'E' to pick up your ingredient. (scroll wheel to move closer / further)");
                break;

            case TutorialState.GoToChoppingBoard:
                if (choppingBoardMarker) choppingBoardMarker.SetActive(true);
                StartTypewriter("Great! Now bring the ingredient to the Chopping board or Mortar. Pick up your knife or pestle respectively and hit the ingredient to start processing");
                break;

            case TutorialState.GoToCauldron:
                if (cauldronMarker) cauldronMarker.SetActive(true);
                StartTypewriter("Move your processed item to the Cauldron and use the spoon to mix the potion");
                break;

            case TutorialState.Complete:
                StartTypewriter("Bottle it up in a potion and your done!! For the full list of Potions press R. Good luck!");
                break;
        }
    }

    

    void StartTypewriter(string textToType)
    {
        currentTargetText = textToType;

        
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeText(currentTargetText));
    }

    IEnumerator TypeText(string textToType)
    {
        isTyping = true;
        tutorialText.text = ""; 

        // Loop through the sentence letter by letter
        foreach (char letter in textToType)
        {
            tutorialText.text += letter;
            yield return new WaitForSeconds(typingSpeed); // Wait a bit before the next letter
        }

        isTyping = false; 
    }

    void SkipTyping()
    {
        //  instantly show the whole sentence
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        tutorialText.text = currentTargetText;
        isTyping = false;
    }
}
