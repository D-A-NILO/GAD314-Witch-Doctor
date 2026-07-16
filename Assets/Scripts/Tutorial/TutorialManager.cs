using System.Collections;
using UnityEngine;
using TMPro;
using System;

[Serializable]
public class TutorialSegment
{
    public MonoTrigger trigger;
    public string text;

}

public class TutorialManager : MonoBehaviour
{
    //public static TutorialManager Instance;

    [Header("UI Reference")]
    public TMP_Text tutorialText;

    [Header("Typewriter Settings")]
    public float typingSpeed = 0.03f; 

    [Header("Visual Markers")]
    public GameObject spawnTableMarker;
    public GameObject choppingBoardMarker;
    public GameObject cauldronMarker;

    //public TutorialState currentState = TutorialState.GoToSpawnTable;
    [SerializeField] private TutorialSegment[] segments;
    private int currentSegment = 0;

   
    private Coroutine typingCoroutine;
    private string currentTargetText = "";
    private bool isTyping = false;

    void Awake()
    {
        //Instance = this;
    }

    void Start()
    {
        //UpdateTutorialProgress();
        StartTypewriter(segments[currentSegment].text);
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
}
