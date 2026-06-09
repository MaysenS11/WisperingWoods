using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    private List<UIDocument> _uiAssets;
    private UIDocument _ingame;
    private UIDocument _dialouge;
    private UIDocument _pause;
    private UIDocument _mainMenu;
    private UIDocument _settings;
    private UIDocument _controls;
    private string _currentUI;

    private VisualElement _pauseRoot;

    void Awake()
    {
        _uiAssets = new List<UIDocument>();
        foreach (var UI in GetComponentsInChildren<UIDocument>(true))
        {
            _uiAssets.Add(UI);
        }
        //Assigning the UI assets to variables for easier access
        _ingame = _uiAssets[0];
        _dialouge = _uiAssets[1];
        _pause = _uiAssets[2];
        _mainMenu = _uiAssets[3];
        _settings = _uiAssets[4];
        _controls = _uiAssets[5];
        
        
        _currentUI = _ingame.name;
        SwitchMenu();
    }

    void OnEnable()
    {
        GameEventManager.Instance.inputEvents.OnEscPressed += EscPressed;
    }

    void OnDisable()
    {
        GameEventManager.Instance.inputEvents.OnEscPressed -= EscPressed;
    }

    void SwitchMenu()
    {
        foreach (UIDocument asset in _uiAssets)
        {
            if (asset.name != _currentUI)
            {
                asset.gameObject.SetActive(false);
            }
        }
    }

    public void EscPressed()
    {
        Debug.Log(_currentUI);
        if (_currentUI == _ingame.name && Time.timeScale == 1 || _currentUI == _dialouge.name)
        {
            EnableMenu(_pause);
            Time.timeScale = 0;
        }
        else if (Time.timeScale == 0)
        {
            EnableMenu(_ingame);
            Time.timeScale = 1;
        }
    }

    void EnableMenu(UIDocument asset)
    {
        Debug.Log(asset.name);
        _currentUI = asset.name;
        asset.gameObject.SetActive(true);
        VisualElement root = asset.rootVisualElement;
        root.SetEnabled(true);
        SwitchMenu();
    }

    public void ButtonMenu(string menuName)
    {
        foreach (UIDocument asset in _uiAssets)
        {
            if (asset.name == menuName)
            {
                EnableMenu(asset);
            }
            else
            {
                Debug.Log($"['{menuName}'] vs ['{asset.name}']");
            }
        }
    }
}
