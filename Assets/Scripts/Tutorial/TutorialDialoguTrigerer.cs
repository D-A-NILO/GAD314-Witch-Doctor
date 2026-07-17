using UnityEngine;

public class TutorialDialoguTrigerer : DialogueTrigger
{
    public MonoTrigger OnDialogueTriggered;

    public override void OnInteract(PlayerInteract playerInteract)
    {
        base.OnInteract(playerInteract);

        OnDialogueTriggered.Trigger();
    }
}
