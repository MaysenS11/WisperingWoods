using System;
using UnityEngine;

public class InputEvents
{
    //Set GameState
    public GameState gameState {get; private set;} = GameState.DEFAULT;


    //Change InputContext
    public void ChangeInputContext(GameState newContext)
    {
        gameState = newContext;
    }

    
    //Initialize InputEventActions
    public event Action<GameState> OnInteractPressed;
    public void InteractPressed()
    {
        OnInteractPressed?.Invoke(gameState);
    }
    
    public event Action OnEscPressed;
    public void EscPressed()
    {
        OnEscPressed?.Invoke();
    }
    
    public event Action<string> OnControlSchemeChanged;
    public void ControlSchemeChanged(string controlScheme)
    {
        OnControlSchemeChanged?.Invoke(controlScheme);
    }
}
