using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

public class ControlsMenu : MonoBehaviour
{
    private Button _resumeButton;
    private MenuManager _parent;
    private void Awake()
    {
        Assert.IsTrue(this.enabled);
        _parent = transform.parent.gameObject.TryGetComponent<MenuManager>(out var menuManager) ? menuManager : null;
    }
    
    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _resumeButton = root.Q<Button>("resumeButton");
        _resumeButton.RegisterCallback<ClickEvent>(ResumeClicked);
    }
    void ResumeClicked(ClickEvent evt)
    {
        _parent.EscPressed();
    }
}
