using System;
using UnityEngine;
using UnityEngine.InputSystem;

public static class PlayerInputHolder
{
    private static PlayerInputActions _actions;

    public static event Action<Vector2> RotatePlayer = delegate { };
    public static event Action GrabAndRelease = delegate { };
    public static event Action<float> ZoomGrabbed = delegate { };
    public static event Action InteractWithObject = delegate { };

    public static void Initialize()
    {
        if (_actions != null)
        {
            RotatePlayer = delegate { };
            GrabAndRelease = delegate { };
            ZoomGrabbed = delegate { };
            InteractWithObject = delegate { };
            _actions.PlayerTransform.Rotate.performed -= (InputAction.CallbackContext context) => RotatePlayer.Invoke(context.ReadValue<Vector2>());
            _actions.WorldInteraction.GrabAndRelease.performed -= (InputAction.CallbackContext context) => GrabAndRelease.Invoke();
            _actions.WorldInteraction.Zoom.performed -= (InputAction.CallbackContext context) => ZoomGrabbed.Invoke(context.ReadValue<float>());
            _actions.WorldInteraction.Interact.performed -= (InputAction.CallbackContext context) => InteractWithObject.Invoke();
            _actions.Dispose();
        }

        _actions = new PlayerInputActions();
        _actions.PlayerTransform.Rotate.performed += (InputAction.CallbackContext context) => RotatePlayer.Invoke(context.ReadValue<Vector2>());
        _actions.WorldInteraction.GrabAndRelease.performed += (InputAction.CallbackContext context) => GrabAndRelease.Invoke();
        _actions.WorldInteraction.Zoom.performed += (InputAction.CallbackContext context) => ZoomGrabbed.Invoke(context.ReadValue<float>());
        _actions.WorldInteraction.Interact.performed += (InputAction.CallbackContext context) => InteractWithObject.Invoke();
        Enable();
    }

    public static void Enable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _actions.Enable();
    }

    public static void Disable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _actions.Disable();
    }
}