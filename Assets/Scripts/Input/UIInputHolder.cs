using System;
using UnityEngine.InputSystem;

public static class UIInputHolder
{
    private static UIInputActions _actions;
    public static event Action Switch = delegate { };

    public static void Initialize()
    {
        if (_actions != null)
        {
            Switch = delegate { };
            _actions.Menu.Switch.performed -= (InputAction.CallbackContext context) => Switch.Invoke();
            _actions.Dispose();
        }

        _actions = new UIInputActions();
        _actions.Menu.Switch.performed += (InputAction.CallbackContext context) => Switch.Invoke();
        _actions.Enable();
    }

    public static void Enable() => _actions.Enable();

    public static void Disable() => _actions.Disable();
}