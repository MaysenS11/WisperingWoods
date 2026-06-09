using System;
using Unity.VisualScripting;
using UnityEngine;

public class Npc : MonoBehaviour
{
    private bool _canInteract;

    void OnEnable()
    {
        GameEventManager.Instance.inputEvents.OnInteractPressed += Interact;
        GameEventManager.Instance.inputEvents.OnSubmitPressed += Submit;
    }
    void Interact()
    {
        if (_canInteract)
        {
            MenuManager.Instance.SetMenu(MenuManager.MenuState.Dialouge);
        }
    }
    void Submit()
    {
        Debug.Log("Submit Pressed");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        _canInteract = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        _canInteract = false;
    }
}
