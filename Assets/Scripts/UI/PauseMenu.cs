using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
   private Button _resumeButton;
   private Button _settingsButton;
   private Button _controlsButton;
   private Button _quitButton;

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
      _settingsButton = root.Q<Button>("settingsButton");
      _controlsButton = root.Q<Button>("controlsButton");
      _quitButton = root.Q<Button>("quitButton");

      _resumeButton.RegisterCallback<ClickEvent>(ResumeClicked);
      _settingsButton.RegisterCallback<ClickEvent>(SettingsClicked);
      _controlsButton.RegisterCallback<ClickEvent>(ControlsClicked);
      _quitButton.RegisterCallback<ClickEvent>(QuitClicked);
   }

   void ResumeClicked(ClickEvent evt)
   {
      _parent.EscPressed();
   }
   void SettingsClicked(ClickEvent evt)
   {
      _parent.SetPauseMenu(MenuManager.PauseMenuState.Settings);
   }
   void ControlsClicked(ClickEvent evt)
   {
      _parent.SetPauseMenu(MenuManager.PauseMenuState.Controls);
   }
   void QuitClicked(ClickEvent evt)
   {
      SceneManager.LoadScene("MainMenu");
   }
}
