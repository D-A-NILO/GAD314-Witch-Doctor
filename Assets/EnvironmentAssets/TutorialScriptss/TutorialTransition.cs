using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTransition : MonoBehaviour
{
    public Image fadeImage; 
    public float fadeSpeed = 2f;
    public TutorialMain tutManager;

    [Header("Stage Objects")]
    public GameObject stage1Objects; 
    public GameObject stage2Objects; 
    public GameObject stage3Objects; 

    public void GoToStage(int stageNumber)
    {
        StartCoroutine(FadeAndSwap(stageNumber));
    }

    private IEnumerator FadeAndSwap(int stage)
    {
        // Fade to black
        Color c = fadeImage.color;
        while (c.a < 1f)
        {
            c.a += Time.deltaTime * fadeSpeed;
            fadeImage.color = c;
            yield return null;
        }

        // Clean up any mess the player made
        tutManager.DeleteAllIngredients(); 

        
        stage1Objects.SetActive(stage == 1);
        stage2Objects.SetActive(stage == 2);
        stage3Objects.SetActive(stage == 3);

        // Fade back to game
        while (c.a > 0f)
        {
            c.a -= Time.deltaTime * fadeSpeed;
            fadeImage.color = c;
            yield return null;
        }
    }

    
}
