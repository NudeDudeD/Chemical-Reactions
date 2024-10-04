using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHolder : MonoBehaviour
{
    private PlayerInputActions _actions;
    private InputAction _rotatePlayer;
    private InputAction _grabAndRelease;
    private InputAction _zoomGrabbed;
    private InputAction _interactWithObject;

    public InputAction RotatePlayer => _rotatePlayer;
    public InputAction GrabAndRelease => _grabAndRelease;
    public InputAction ZoomGrabbed => _zoomGrabbed;
    public InputAction InteractWithObject => _interactWithObject;

    private void Awake()
    {
        _actions = new PlayerInputActions();
        _rotatePlayer = _actions.PlayerTransform.Rotate;
        _grabAndRelease = _actions.WorldInteraction.GrabAndRelease;
        _zoomGrabbed = _actions.WorldInteraction.Zoom;
        _interactWithObject = _actions.WorldInteraction.Interact;
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _actions.Enable();
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _actions.Disable();
    }
}