using UnityEngine;

public class TutorialZoneTrigger : MonoBehaviour
{
    [Tooltip("Match this to what the player is supposed to accomplish here")]
    public TutorialState associatedState;

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the zone is the player
        if (other.CompareTag("Player"))
        {
            if (TutorialManager.Instance != null)
            {
                TutorialManager.Instance.OnPlayerStepOnMarker(associatedState);
            }
        }
    }
}
