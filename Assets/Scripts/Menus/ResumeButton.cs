using UnityEngine;
using UnityEngine.UIElements;

public class ResumeButton : MonoBehaviour
{
    private MenuManager _parent;
    private void Awake()
    {
        _parent = transform.parent.gameObject.TryGetComponent<MenuManager>(out var menuManager) ? menuManager : null;
    }
    
    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        Button _resumeButton = root.Q<Button>("resumeButton");
        if (_resumeButton != null) _resumeButton.RegisterCallback<ClickEvent>(ResumeClicked);
    }

    void ResumeClicked(ClickEvent evt)
    {
        if (_parent.GetCurrentMenu() == MenuManager.MenuState.MainMenu) 
            _parent.SetPauseMenu(MenuManager.PauseMenuState.Disabled);
        else
            _parent.SetPauseMenu(MenuManager.PauseMenuState.Main);
    }
}
