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

        if(paused)
            OnPauseAction?.Invoke();
        else
            OnUnPauseAction?.Invoke();
    }

    public void Quit()
    {
        Application.Quit();
    }

}
