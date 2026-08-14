using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    public InputActionReference pauseAction;
    public PauseMenu pauseMenu;
    //private bool paused;
    public IMenu activeMenu;
    bool nonPauseMenusFreeze = false;

    public static MenuManager I;
    
    public UnityEvent OnPauseAction;
    public UnityEvent OnUnPauseAction;

    void Awake()
    {
        if(I == null)
        {
            I = this;
        }else
        {
            Destroy(I.gameObject);
        }
    }

    void OnEnable()
    {
        pauseAction.action.performed += onPausePressed;

        pauseAction.action.Enable();
    }

    void OnDisable()
    {
      pauseAction.action.performed -= onPausePressed;  
    }

    
    

    private void onPausePressed(InputAction.CallbackContext context)
    {
        if(activeMenu == null)
        {
            ShowMenu(pauseMenu);
        }
        else
        {
            HideMenu(activeMenu);
        }

    }

    public void Quit()
    {
        Time.timeScale = 1f;
        Debug.Log($"time scale: {Time.timeScale}");
        Application.Quit();
    }

    public void TryShowMenu(IMenu menu)
    {
        if(activeMenu == null)
        {
            ShowMenu(menu);
        }
    }
    public void TryHideMenu(IMenu menu)
    {
        if(activeMenu == menu)
        {
            HideMenu(menu);
        }
    }

    private void ShowMenu(IMenu menu)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        menu.OnShow();

        if(menu == (IMenu)pauseMenu)
        {
            FreezeTime(true);
            OnPauseAction?.Invoke();
        }
        else
            if(nonPauseMenusFreeze)
                FreezeTime(true);

        activeMenu = menu;
    }

    private void HideMenu(IMenu menuObj)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        menuObj.OnHide();

        if(menuObj == (IMenu)pauseMenu)
        {
            OnUnPauseAction?.Invoke();
        }

        FreezeTime(false);

        activeMenu = null;
    }

    public bool IsActiveMenu(IMenu menu)
    {
        return activeMenu == menu;
    }

    private void FreezeTime(bool freeze)
    {
        
        Time.timeScale = freeze ? 0f : 1f;
        Debug.Log($"time scale: {Time.timeScale}");
    }
}
