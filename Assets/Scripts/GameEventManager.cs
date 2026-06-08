using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager Instance { get; private set; }

    public static Enum gameState;
    
    public InputEvents inputEvents;
    public PlayerInput PlayerInput;

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
            Instance.inputEvents.InteractPressed();
        }
        if (PlayerInput.actions["Esc"].WasPressedThisFrame())
        {
            Instance.inputEvents.EscPressed();
        }
        
        gameState = inputEvents.gameState;
    }
}
