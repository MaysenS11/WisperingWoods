using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 Movement;
    private PlayerInput PlayerInput;
    private InputAction _moveAction;

    private Vector2 _moveDirection;
    //private bool _jumpPressed;
    //private bool _interactPressed;
    //private bool _submitPressed;
    //private bool _escPressed;
    
    private static InputManager _instance;

    private void Awake()
    {
        PlayerInput = GameEventManager.Instance.PlayerInput;
        if (_instance != null)
        {
            Debug.LogError("There can only be one instance of InputManager");
        }
        _instance = this;
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
        _moveAction = PlayerInput.actions["Move"];
    }

    public static InputManager GetInstance()
    {
        return _instance;
    }

    private void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();
    }

    public void MovePressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _moveDirection = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            _moveDirection = context.ReadValue<Vector2>();
        }
    }
    /*public void JumpPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _jumpPressed = true;
        }
        else if (context.canceled)
        {
            _jumpPressed = false;
        }
    }
    public void InteractPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _interactPressed = true;
        }
        else if (context.canceled)
        {
            _interactPressed = false;
        }
    }
    public void SubmitPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _submitPressed = true;
        }
        else if (context.canceled)
        {
            _submitPressed = false;
        }
    }
    public void EscPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _escPressed = true;
        }
        else if (context.canceled)
        {
            _escPressed = false;
        }
    }*/
}
