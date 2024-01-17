using UnityEngine;
using UnityEngine.InputSystem;

public class UserInputController : MonoBehaviour
{
    
    private static PlayerInput _playerMovement;
    
    public static InputAction _mousePosition;
    public static InputAction _movementAction;
    public static InputAction _leftClick;
    public static InputAction _spaceAction;
    public static InputAction _pause;
    public static InputAction _reload;
    public static InputAction _QButton;

    private void Awake()
    {
        _playerMovement = new();
        _playerMovement.Enable();
        
        _mousePosition = _playerMovement.Player.MousePosition;
        _movementAction = _playerMovement.Player.Movement;
        _leftClick = _playerMovement.Player.LeftClick;
        _spaceAction = _playerMovement.Player.Interact;
        _pause = _playerMovement.Player.Pause;
        _reload = _playerMovement.Player.Reload;
        _QButton = _playerMovement.Player.LookForward;

    }
    
}
