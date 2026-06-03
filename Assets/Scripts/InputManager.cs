using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 Movement;
    public PlayerInput PlayerInput;
    private InputAction _moveAction;

    private void Awake()
    {
        _moveAction = PlayerInput.actions["Move"];
        PlayerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();
    }
}
