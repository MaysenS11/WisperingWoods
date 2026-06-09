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
        //start dialouge
    }
    void Submit(GameState gameState)
    {
        if(!gameState.Equals(GameState.DIALOUGE))
        {
            return;
        }
        ContinueOrExitStory();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        _canInteract = true;
    }

    void ContinueOrExitStory()
    {
        
    }
}
