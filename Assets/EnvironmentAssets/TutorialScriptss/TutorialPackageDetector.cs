using UnityEngine;

public class TutorialPackageDetector : MonoBehaviour
{
   
    public int stepToComplete = 12; 

    private bool isShuttingDown = false;

    
    void OnApplicationQuit()
    {
        isShuttingDown = true;
    }

    void OnDestroy()
    {
        if (isShuttingDown) return;

        
        TutorialMain manager = FindAnyObjectByType<TutorialMain>();

        if (manager != null)
        {
            manager.CompleteStep(stepToComplete);
        }
        else
        {
            Debug.LogWarning("Package opened, but no TutorialMain manager was found in the scene!");
        }
    }
}
