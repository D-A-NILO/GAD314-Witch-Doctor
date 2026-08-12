using UnityEngine;
using UnityEngine.Events; 
public class TutorialShopTriggerer : ShopTrigger
{
    [Header("Tutorial Hooks")]
    public UnityEvent OnShopOpenedEvent; 

    public override void OnInteract(PlayerInteract interactor)
    {
        base.OnInteract(interactor); 

        OnShopOpenedEvent?.Invoke(); 
    }
}
