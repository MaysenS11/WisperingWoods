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
    private Dictionary<MenuState, UIDocument> _menus = new Dictionary<MenuState, UIDocument>();

    public enum MenuState
    {
        Ingame,
        Dialouge,
        Pause,
        MainMenu,
        Settings,
        Controls
    }

    void Awake()
    {
        _menus[MenuState.Ingame] = ingame;
        _menus[MenuState.Dialouge] = dialouge;
        _menus[MenuState.Pause] = pause;
        _menus[MenuState.MainMenu] = mainMenu;
        _menus[MenuState.Settings] = settings;
        _menus[MenuState.Controls] = controls;

        SetMenu(MenuState.Ingame);
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
        if (_currentUI == MenuState.Ingame || _currentUI == MenuState.Dialouge) {
            SetMenu(MenuState.Pause);
            Time.timeScale = 0;
        }
        else if (_currentUI == MenuState.Pause)
        {
            SetMenu(MenuState.Ingame);
            Time.timeScale = 1;
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
}
