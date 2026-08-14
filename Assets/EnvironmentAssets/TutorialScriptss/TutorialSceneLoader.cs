using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialSceneLoader : MonoBehaviour
{
    [Header("Scene Settings")]
    
    public string mainGameSceneName = "Game";

    [Header("Fade Settings (Optional)")]
    public Image fadeImage;
    public float fadeSpeed = 2f;

    
    public void LoadMainGame()
    {
        StartCoroutine(TransitionToMainGame());
    }

    private IEnumerator TransitionToMainGame()
    {
        
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            while (c.a < 1f)
            {
                c.a += Time.deltaTime * fadeSpeed;
                fadeImage.color = c;
                yield return null;
            }
        }

        // Load the main game scene
        SceneManager.LoadScene(mainGameSceneName);
    }
}
