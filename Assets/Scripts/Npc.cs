using System;
using Unity.VisualScripting;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] private string startKnot;
    private bool _canInteract;

    void OnEnable()
    {
        GameEventManager.Instance.inputEvents.OnInteractPressed += Interact;
    }

    void OnDisable()
    {
        if (GameEventManager.Instance != null && GameEventManager.Instance.inputEvents != null)
        {
            GameEventManager.Instance.inputEvents.OnInteractPressed -= Interact;
        }
    }

    void Interact()
    {
        if (_canInteract)
        {
            MenuManager.Instance.SetMenu(MenuManager.MenuState.Dialouge);
            DialogueManager.Instance.StartDialogue(startKnot);
        }
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
