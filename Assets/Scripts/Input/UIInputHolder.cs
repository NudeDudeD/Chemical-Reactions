using UnityEngine;
using UnityEngine.InputSystem;

public class UIInputHolder : MonoBehaviour
{
    private UIInputActions _actions;
    private InputAction _switch;

    public InputAction Switch => _switch;

    private void Awake()
    {
        _actions = new UIInputActions();
        _switch = _actions.Menu.Switch;
    }

    private void OnEnable()
    {
        _actions.Enable();
    }

    private void OnDisable()
    {
        _actions.Disable();
    }
}