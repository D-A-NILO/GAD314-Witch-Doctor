using UnityEngine;

public class TutorialShopTriggerer : ShopTrigger
{

    public MonoTrigger OnShopOpened;
    public override void OnInteract(PlayerInteract interactor)
    {
        base.OnInteract(interactor);

        OnShopOpened.Trigger();
    }
}
