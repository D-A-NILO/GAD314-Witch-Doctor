using UnityEngine;
using TMPro;
using System.Collections;

public class Dialogue : MonoBehaviour
{
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public NPCIllness illness;
    public float textSpeed;
    public float textHoldTime = 2f;

    public NPCMovement npcMovement;

    private string[] lines;
    private int textIndex;
    private bool dialogueActive;
    private bool waitingToFinish;
    private Coroutine typingCoroutine;
    public System.Action onDialogueEnd;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (dialogueBox == null)
        {
            dialogueBox = GameObject.Find("DialogueBox");
        }
        if (dialogueText == null)
        {
            dialogueText = dialogueBox.GetComponentInChildren<TextMeshProUGUI>();
        }

        dialogueText.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dialogueActive) return;

            

        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == lines[textIndex])
            {
                NextLine();
            }
            else
            { 
                StopAllCoroutines();
                dialogueText.text = lines[textIndex];
            }
        }
    }

    public void StartDialogue()
    {
        if (dialogueActive) return;

        dialogueActive = true;
        dialogueBox.SetActive(true);
        textIndex = 0;
        dialogueText.text = string.Empty;

        typingCoroutine = StartCoroutine(StartTypingNextFrame());
    }

    public void NextLine()
    {
        if (!illness.isCured && textIndex == 0)
        {
            EndDialogue();
            return;
        } 

        if (textIndex < lines.Length - 1)
        {
            textIndex++;
            dialogueText.text = string.Empty;
            typingCoroutine = StartCoroutine(TypeLine());
        }
        else
        { 
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        dialogueActive = false;
        dialogueBox.SetActive(false);
        dialogueText.text = string.Empty;

        onDialogueEnd?.Invoke();
        onDialogueEnd = null;

        //if (npcMovement != null)
        { 
            //npcMovement.ContinueToNextPoint();
        }
    }

    public void SetDialogueSets(string[] newLines)
    {
        lines = newLines;
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[textIndex].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds( 1f / textSpeed);
        }


        if (textIndex >= lines.Length - 1)
        {
            yield return new WaitForSeconds(textHoldTime);
            EndDialogue();
        }
        typingCoroutine = null;
    }

    IEnumerator StartTypingNextFrame()
    {
        yield return null;

        typingCoroutine = StartCoroutine(TypeLine());
    }

    public bool IsDialogueActive => dialogueActive;
}
