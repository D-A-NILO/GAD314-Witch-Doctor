using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public InputActionReference pauseAction;
    public GameObject pauseMenu;
    private bool paused;
    public UnityEvent OnPauseAction;
    public UnityEvent OnUnPauseAction;

    void OnEnable()
    {
        pauseAction.action.performed += TogglePause;

        pauseAction.action.Enable();
    }

    void OnDisable()
    {
      pauseAction.action.performed -= TogglePause;  
    }

    

    private void TogglePause(InputAction.CallbackContext context)
    {
        paused = !paused;

        pauseMenu.SetActive(paused);
        Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = paused;

        Time.timeScale = paused ? 0f : 1f;
        Debug.Log($"time scale: {Time.timeScale}");

        if(paused)
        {
            OnPauseAction?.Invoke();
        }
        else
        {
            OnUnPauseAction?.Invoke();
        }
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        Debug.Log($"time scale: {Time.timeScale}");
        Application.Quit();
    }

}
