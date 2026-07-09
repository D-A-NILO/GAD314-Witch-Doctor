using UnityEngine;
using TMPro;
using System.Collections;

public class Dialogue : MonoBehaviour
{
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public DialogueSet[] dialogueSets;
    public float textSpeed;

    public NPCMovement npcMovement;

    private int currentDialogueSet;
    private string[] lines;
    private int textIndex;
    private bool dialogueActive;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueBox.SetActive(false);
        dialogueText.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dialogueActive)
            return;

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

    public void StartDialogue(int index)
    {
        lines = dialogueSets[index].lines;
        dialogueActive = true;
        dialogueBox.SetActive(true);
        textIndex = 0;
        dialogueText.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    public void NextLine()
    {
        if (textIndex < lines.Length - 1)
        {
            textIndex++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        { 
            EndDialogue();
        }
    }

    void EndDialogue()
    { 
        dialogueActive = false;
        dialogueBox.SetActive(false);
        dialogueText.text = string.Empty;

        if (npcMovement != null)
        { 
            npcMovement.ContinueToNextPoint();
        }
    }

    public void SetDialogueSet(int lineIndex)
    {
        if (lineIndex >= 0 && lineIndex < dialogueSets.Length)
        { 
            currentDialogueSet = lineIndex;
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[textIndex].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
