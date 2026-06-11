using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class EndMenu : MonoBehaviour
{
   private Button _quitButton;
   
   private void Awake()
   {
      Assert.IsTrue(this.enabled);
   }
   void OnEnable()
   {
      var root = GetComponent<UIDocument>().rootVisualElement;
      _quitButton = root.Q<Button>("quitButton");
      _quitButton.RegisterCallback<ClickEvent>(QuitClicked);
   }
   
   void QuitClicked(ClickEvent evt)
   {
      SceneManager.LoadScene("MainMenu");
   }
}
