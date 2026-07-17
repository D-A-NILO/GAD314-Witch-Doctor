using UnityEngine;

public class GrabbableTriggerer : Grabbable
{
    public MonoTrigger trigger;
    public override void OnInteract(PlayerInteract interactor)
    {
        base.OnInteract(interactor);

        trigger.Trigger();
    }
}
