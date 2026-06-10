using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    [Header("Settings")]
    public MenuState startingMenu;

    [Header("UI Documents")]
    public UIDocument ingame;
    public UIDocument dialouge;
    public UIDocument pause;
    public UIDocument mainMenu;
    public UIDocument settings;
    public UIDocument controls;
    public UIDocument credits;
    public UIDocument transition;

    private MenuState _currentUI;
    private PauseMenuState _currentPauseMenuState;
    private Dictionary<MenuState, UIDocument> _menus = new Dictionary<MenuState, UIDocument>();
    private Dictionary<PauseMenuState, UIDocument> _pauseMenus = new Dictionary<PauseMenuState, UIDocument>();
    private VisualElement _fadeOverlay;

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
        Controls,
        Credits
    }

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There can only be one instance of MenuManager");
        }
        Instance = this;

        _menus[MenuState.Ingame] = ingame;
        _menus[MenuState.Dialouge] = dialouge;
        _menus[MenuState.MainMenu] = mainMenu;

        _pauseMenus[PauseMenuState.Disabled] = null;
        _pauseMenus[PauseMenuState.Main] = pause;
        _pauseMenus[PauseMenuState.Settings] = settings;
        _pauseMenus[PauseMenuState.Controls] = controls;
        _pauseMenus[PauseMenuState.Credits] = credits;
        
        SetMenu(startingMenu);
        SetPauseMenu(PauseMenuState.Disabled);
        
        _fadeOverlay = transition.rootVisualElement.Q<VisualElement>("fade-overlay");
        _fadeOverlay.style.opacity = 0f;
        _fadeOverlay.style.display = DisplayStyle.None;
    }

    void OnEnable()
    {
        GameEventManager.Instance.inputEvents.OnEscPressed += EscPressed;
    }

    void OnDisable()
    {
        GameEventManager.Instance.inputEvents.OnEscPressed -= EscPressed;
    }

    public IEnumerator FadeRoutine(float targetAlpha, float duration)
    {
        if (_fadeOverlay == null) yield break;

        if (targetAlpha > 0f)
        {
            _fadeOverlay.style.display = DisplayStyle.Flex;
        }

        float startAlpha = _fadeOverlay.style.opacity.value;
        float time = 0f;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(time / duration);
            _fadeOverlay.style.opacity = Mathf.SmoothStep(startAlpha, targetAlpha, t);
            yield return null;
        }

        _fadeOverlay.style.opacity = targetAlpha;

        if (targetAlpha <= 0f)
        {
            _fadeOverlay.style.display = DisplayStyle.None;
        }
    }

    public void EscPressed()
    {
        if (_currentUI == MenuState.MainMenu) return;
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
            keyValuePair.Value.gameObject.SetActive(keyValuePair.Key == _currentUI);
        }
    }

    public void SetPauseMenu(PauseMenuState menu)
    {
        _currentPauseMenuState = menu;
        foreach (var keyValuePair in _pauseMenus)
        {
            if (keyValuePair.Value != null) {
                keyValuePair.Value.gameObject.SetActive(keyValuePair.Key == _currentPauseMenuState);
            }
        }
    }

    public MenuState GetCurrentMenu() { return _currentUI; }
    public PauseMenuState GetCurrentPauseMenu() { return _currentPauseMenuState; }
}