using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    public UIDocument ingame;
    public UIDocument dialouge;
    public UIDocument pause;
    public UIDocument mainMenu;
    public UIDocument settings;
    public UIDocument controls;
    private MenuState _currentUI;
    private PauseMenuState _currentPauseMenuState;
    private Dictionary<MenuState, UIDocument> _menus = new Dictionary<MenuState, UIDocument>();
    private Dictionary<PauseMenuState, UIDocument> _pauseMenus = new Dictionary<PauseMenuState, UIDocument>();

    public enum MenuState
    {
        Ingame,
        Dialouge,
        MainMenu
    }

    public enum PauseMenuState
    {
        Disabled,
        Main,
        Settings,
        Controls
    }

    void Awake()
    {
        _menus[MenuState.Ingame] = ingame;
        _menus[MenuState.Dialouge] = dialouge;
        _menus[MenuState.MainMenu] = mainMenu;

        _pauseMenus[PauseMenuState.Disabled] = null;
        _pauseMenus[PauseMenuState.Main] = pause;
        _pauseMenus[PauseMenuState.Settings] = settings;
        _pauseMenus[PauseMenuState.Controls] = controls;

        SetMenu(MenuState.Ingame);
        SetPauseMenu(PauseMenuState.Disabled);
    }

    void OnEnable()
    {
        GameEventManager.Instance.inputEvents.OnEscPressed += EscPressed;
    }

    void OnDisable()
    {
        GameEventManager.Instance.inputEvents.OnEscPressed -= EscPressed;
    }

    public void EscPressed()
    {
        if (_currentPauseMenuState == PauseMenuState.Disabled) {
            Time.timeScale = 0;
            SetPauseMenu(PauseMenuState.Main);
        } else {
            Time.timeScale = 1;
            SetPauseMenu(PauseMenuState.Disabled);
        }
    }

    public void SetMenu(MenuState menu)
    {
        _currentUI = menu;
        foreach (var keyValuePair in _menus)
        {
            if (keyValuePair.Key != _currentUI)
            {
                keyValuePair.Value.gameObject.SetActive(false);
            }
            else
            {
                keyValuePair.Value.gameObject.SetActive(true);
            }
        }
    }

    public void SetPauseMenu(PauseMenuState menu)
    {
        _currentPauseMenuState = menu;
        foreach (var keyValuePair in _pauseMenus)
        {
            if (keyValuePair.Key != menu)
            {
                if (keyValuePair.Value != null)
                {
                    keyValuePair.Value.gameObject.SetActive(false);
                }
            }
            else
            {
                if (keyValuePair.Value != null)
                {
                    keyValuePair.Value.gameObject.SetActive(true);
                }
            }
        }
    }
}
