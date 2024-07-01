using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerControls _playerControls;

    public InputAction RotatePlayer { get; private set; }
    public InputAction GrabAndRelease{ get; private set; }
    public InputAction ZoomGrabbed { get; private set; }

    private void Awake()
    {
        _playerControls = new PlayerControls();
        RotatePlayer = _playerControls.PlayerTransform.Rotate;
        GrabAndRelease = _playerControls.WorldInteraction.GrabAndRelease;
        ZoomGrabbed = _playerControls.WorldInteraction.Zoom;
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }
}
