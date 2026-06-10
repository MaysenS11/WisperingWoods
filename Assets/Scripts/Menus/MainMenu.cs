using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    /*[SerializeField] UIDocument mainMenuDocument;

    private Button StartButton;
    private Button OptionsButton;
    private Button CreditsButton;
    private Button QuitButton;

    void Awake()
    {
        VisualElement root = mainMenuDocument.rootVisualElement;
        
        StartButton = root.Q<Button>("StartButton");
        OptionsButton = root.Q<Button>("OptionsButton");
        CreditsButton = root.Q<Button>("CreditsButton");
        QuitButton = root.Q<Button>("QuitButton");
        
        StartButton.clickable.clicked += StartGame;
        OptionsButton.clickable.clicked += OpenOptions;
        CreditsButton.clickable.clicked += OpenCredits;
        QuitButton.clickable.clicked += QuitGame;
    }

    void StartGame()
    {
        SceneManager.LoadScene("Level");
    }
    
    void OpenOptions()
    {
        print("Open Options");
    }
    
    void OpenCredits()
    {
        print("Open Credits");
    }
    
    void QuitGame()
    {
        Application.Quit();
    }*/
}
