using UnityEngine;
using UnityEngine.UIElements;

public class BackButton : MonoBehaviour
{
    private MenuManager _parent;
    private void Awake()
    {
        _parent = transform.parent.gameObject.TryGetComponent<MenuManager>(out var menuManager) ? menuManager : null;
    }
    
    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        Button _backButton = root.Q<Button>("backButton");
        if (_backButton != null) _backButton.RegisterCallback<ClickEvent>(BackClicked);
    }

    void BackClicked(ClickEvent evt)
    {
        if (_parent.GetCurrentMenu() == MenuManager.MenuState.MainMenu) 
            _parent.SetPauseMenu(MenuManager.PauseMenuState.Disabled);
        else
            _parent.SetPauseMenu(MenuManager.PauseMenuState.Main);
    }
}
