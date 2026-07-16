using System.Linq;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class AreaTrigger : MonoTrigger
{
    public string[] restrictToTags;
    public LayerMask triggerMask = int.MaxValue; // everything


    void OnTriggerEnter(Collider other)
    {
        if(restrictToTags.Length > 0) // if restricting tags
            if(!restrictToTags.Contains(other.tag)) // if not found
                return;

        if((triggerMask & (1 << other.gameObject.layer)) == 0) // if other layer is not in layer mask
            return;
        
        Trigger();
    }
}
