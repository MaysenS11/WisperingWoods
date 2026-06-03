using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private bool _canInteract;
    void OnEnable()
    {
        GameEventManager.Instance.inputEvents.OnInteractPressed += InteractPressed;
    }
    void OnDisable()
    {
        GameEventManager.Instance.inputEvents.OnInteractPressed -= InteractPressed;
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        _canInteract = true;
    }
    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        _canInteract = false;
    }
    void InteractPressed(GameState gameState)
    {
        Debug.Log("Interacted with " + name);
        if (_canInteract)
        {
            Interact();
        }
    }

    public abstract void Interact();
}





