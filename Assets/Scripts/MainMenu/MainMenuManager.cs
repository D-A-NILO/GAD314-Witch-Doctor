using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string tutorialSceneName = "Tutorial";
    [SerializeField] private string playSceneName;

    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject opasityThing;

    public void PlayTutorial()
    {
        SceneManager.LoadScene(tutorialSceneName);
    }

    public void Play()
    {
        if (string.IsNullOrEmpty(playSceneName))
        {
            Debug.LogWarning("MainMenuManager: no play scene assigned yet");
            return;
        }

        SceneManager.LoadScene(playSceneName);
    }

    public void OpenSettings()
    {
        opasityThing.SetActive(true);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        opasityThing.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void Quit()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}
