using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    public Dialogue dialogue;
    public NPCMovement npcMovement;
    public void OnInteract(PlayerInteract playerInteract)
    {
        dialogue.StartDialogue(npcMovement.CurrentPoint);
    }
}
