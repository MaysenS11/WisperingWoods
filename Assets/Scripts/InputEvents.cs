using System;
using UnityEngine;

public class InputEvents
{
    public event Action OnInteractPressed;
    public void InteractPressed()
    {
        OnInteractPressed?.Invoke();
    }
    
    public event Action OnSubmitPressed;
    public void SubmitPressed()
    {
        OnSubmitPressed?.Invoke();
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
