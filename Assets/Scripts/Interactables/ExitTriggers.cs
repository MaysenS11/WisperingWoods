using UnityEngine;

public class ExitTriggers : MonoBehaviour
{
    [SerializeField] private string startKnot;
    [SerializeField] private GameObject player;
    [SerializeField] private BoxCollider2D exit;
    private bool _canTrigger = true;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _canTrigger)
        {
            MenuManager.Instance.SetMenu(MenuManager.MenuState.Dialouge);
            DialogueManager.Instance.StartDialogue(startKnot);
        }
        if (startKnot == "State_1")
        {
            _canTrigger = false;
            exit.enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (_canTrigger == false)
        {
            if (player.transform.position.x > transform.position.x)
            {
                startKnot = "State_1_1";
                exit.enabled = true;
            }
            _canTrigger = true;
        }
    }
}
