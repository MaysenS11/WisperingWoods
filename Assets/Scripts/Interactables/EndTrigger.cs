using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        MenuManager.Instance.SetPauseMenu(MenuManager.PauseMenuState.End);
        SceneManager.LoadScene("EndMenu");
        Debug.Log(SceneManager.GetActiveScene().name);
    }
}
