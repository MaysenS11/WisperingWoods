using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager Instance { get; private set; }

    public static Enum gameState;
    
    public PlayerInput PlayerInput;
    
    public InputEvents inputEvents;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There can only be one instance of GameEventManager");
        }
        
        Instance = this;
        
        PlayerInput = GetComponent<PlayerInput>();
        inputEvents = new InputEvents();
    }

    void Update()
    {
        if (PlayerInput.actions["Interact"].triggered)
        {
            Debug.Log("Interacted");
        }
        
        gameState = inputEvents.gameState;
    }
}
