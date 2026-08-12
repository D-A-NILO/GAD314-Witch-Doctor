using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
public class TutorialStep
{
    [TextArea(2, 4)]
    public string instructionText;
    
    public bool autoAdvance;
    public float delayTime;

    
    public UnityEvent OnStepStart;
}

public class TutorialMain : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI tutorialText;

    [Header("Tutorial Flow")]
    public TutorialStep[] steps;
    private int currentStepIndex = 0;

    private Coroutine temporaryMessageRoutine;

    void Start()
    {
        if (steps.Length > 0)
        {
            PlayStep(0);
        }
    }

    public void PlayStep(int index)
    {
        if (index >= steps.Length)
        {
            tutorialText.text = "Tutorial Complete! Loading Next Area...";
            return;
        }

        currentStepIndex = index;
        tutorialText.text = steps[currentStepIndex].instructionText;
        steps[currentStepIndex].OnStepStart?.Invoke();

        if (steps[currentStepIndex].autoAdvance)
        {
            StartCoroutine(AutoAdvanceRoutine(steps[currentStepIndex].delayTime));
        }
    }

    
    public void CompleteStep(int expectedStepIndex)
    {
        
        if (currentStepIndex == expectedStepIndex)
        {
            StopAllCoroutines();
            PlayStep(currentStepIndex + 1);
        }
        else
        {
            Debug.Log($"Ignored trigger for Step {expectedStepIndex} because player is on Step {currentStepIndex}");
        }
    }

    public void ShowTemporaryMessage(string message)
    {
        if (temporaryMessageRoutine != null) StopCoroutine(temporaryMessageRoutine);
        temporaryMessageRoutine = StartCoroutine(TempMessageRoutine(message, 3f));
    }

    private IEnumerator AutoAdvanceRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        CompleteStep(currentStepIndex);
    }

    private IEnumerator TempMessageRoutine(string message, float duration)
    {
        tutorialText.text = $"<color=red>{message}</color>";
        yield return new WaitForSeconds(duration);
        tutorialText.text = steps[currentStepIndex].instructionText;
    }

    public void DeleteAllIngredients()
    {
        foreach (Ingredient ingredient in FindObjectsByType<Ingredient>(FindObjectsSortMode.None))
        {
            Destroy(ingredient.gameObject);
        }
    }
}
