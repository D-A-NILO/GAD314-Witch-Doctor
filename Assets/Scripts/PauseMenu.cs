using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour, IMenu
{
    public GameObject pauseMenu;

    public void OnHide()
    {
        pauseMenu.SetActive(false);
    }

    public void OnShow()
    {
        pauseMenu.SetActive(true);
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        Debug.Log($"time scale: {Time.timeScale}");
        Application.Quit();
    }

}
