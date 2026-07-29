using System.Collections;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

[Serializable]
public class TutorialSegment
{
    public MonoTrigger trigger;
    [HideInInspector] public UnityEvent executeOnSegment;
    public string text;

}

public class TutorialManager : MonoBehaviour
{
    //public static TutorialManager Instance;

    [Header("UI Reference")]
    public TMP_Text tutorialText;

    [Header("Typewriter Settings")]
    public float typingSpeed = 0.03f; 


    //public TutorialState currentState = TutorialState.GoToSpawnTable;
    [SerializeField] private TutorialSegment[] segments;
    private int currentSegment = 0;

    public bool orderTriggerEvents = true;
   
    private Coroutine typingCoroutine;
    private string currentTargetText = "";
    private bool isTyping = false;

    void Awake()
    {
        //Instance = this;
    }

    void Start()
    {
        if(orderTriggerEvents)
        {
            foreach(TutorialSegment segment in segments)
            {
                if(segment.trigger == null) continue;
                segment.executeOnSegment = segment.trigger.OnTriggered;
                segment.trigger.OnTriggered = new();
            }
        }
        //UpdateTutorialProgress();
        currentSegment = -1;
        Continue();
    }

    void Update()
    {
        // If the text is currently typing, allow the player to skip it by pressing Space or Left Click
        if (isTyping && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            SkipTyping();
        }
    }

    private bool EOS //end of segments
    {
        get => currentSegment + 1 >= segments.Length;
    }

    public void Continue()
    {
       
        if(EOS) return;

        currentSegment++;
        //unsubscribe current trigger
        segments[currentSegment].trigger?.RemoveListener(Continue);
        segments[currentSegment].executeOnSegment?.Invoke();

        StartTypewriter(segments[currentSegment].text);
        
        if(EOS) // if end of tutorial
        {
            HideAfterDelay(10f);
        }
        else
        {
            // subscribe to next trigger
            segments[currentSegment+1].trigger.AddListener(Continue);
        }

    }

    private IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Tutorial hidden");
        tutorialText.transform.parent.gameObject.SetActive(false); // disable text parent
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

    public void DeleteAllIngredients()
    {
        foreach(Ingredient ingredient in FindObjectsByType<Ingredient>(FindObjectsSortMode.None))
        {
            Destroy(ingredient.gameObject);
        }
    }
}
