using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.Assertions;
using Cursor = UnityEngine.Cursor;

public class MainMenu : MonoBehaviour
{
    private Button _startButton;
    private Button _settingsButton;
    private Button _creditsButton;
    private Button _quitButton;
    
    private MenuManager _parent;

    void Awake()
    {
        Assert.IsTrue(this.enabled);
        _parent = transform.parent.gameObject.TryGetComponent<MenuManager>(out var menuManager) ? menuManager : null;
        
        var root = GetComponent<UIDocument>().rootVisualElement;
        _startButton = root.Q<Button>("startButton");
        _settingsButton = root.Q<Button>("settingsButton");
        _creditsButton = root.Q<Button>("creditsButton");
        _quitButton = root.Q<Button>("quitButton");

        _startButton.RegisterCallback<ClickEvent>(StartClicked);
        _settingsButton.RegisterCallback<ClickEvent>(SettingsClicked);
        _creditsButton.RegisterCallback<ClickEvent>(CreditsClicked);
        _quitButton.RegisterCallback<ClickEvent>(QuitClicked);
        
        if (UnityEngine.EventSystems.EventSystem.current != null)
        {
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        }
    }

    void OnEnable()
    {
        SoundManager.Instance.CurrentMusicMode = SoundManager.MusicMode.InMenuMusic;
    }

    void StartClicked(ClickEvent evt)
    {
        Cursor.visible = false;
        SceneManager.LoadScene("Level");
        SoundManager.Instance.CurrentMusicMode = SoundManager.MusicMode.InGameMusic;
        Time.timeScale = 1;
    }

    void SettingsClicked(ClickEvent evt)
    {
        _parent.SetPauseMenu(MenuManager.PauseMenuState.Settings);
    }

    void CreditsClicked(ClickEvent evt)
    {
        _parent.SetPauseMenu(MenuManager.PauseMenuState.Credits);
    }

    void QuitClicked(ClickEvent evt)
    {
        Application.Quit();
    }
}
