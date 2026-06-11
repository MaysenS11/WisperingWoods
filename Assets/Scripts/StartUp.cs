using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUp : MonoBehaviour
{
    void Awake()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
