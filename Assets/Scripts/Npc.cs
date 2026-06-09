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
        Debug.Log("Interact Pressed");
        if (_canInteract)
        {
            Debug.Log("Interacting with NPC");
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
        Debug.Log("Player entered NPC interaction range");
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        _canInteract = false;
    }
}
