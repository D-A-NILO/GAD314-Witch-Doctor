using System.ComponentModel;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private Dialogue dialogue;
    public NPCMovement npcMovement;
    public InteractText interactText;


    private void Start()
    {
        Debug.Log($"is dialogue assigned: {dialogue}");
    }
    public virtual void OnInteract(PlayerInteract playerInteract)
    {
        dialogue.StartDialogue();
        interactText.text.SetActive(false);
    }

    public void SetDialogue(Dialogue dialogueRef)
    {
        dialogue = dialogueRef;
    }
}
