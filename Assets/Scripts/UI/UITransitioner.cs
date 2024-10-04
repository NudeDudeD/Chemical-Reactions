using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
    
public class UITransitioner : MonoBehaviour
{
    [SerializeField] protected UIInputHolder _inputActions;
    [SerializeField] protected UITransitioner _switchTo;
    [SerializeField] protected UnityEvent _onSwitch;

    protected static UITransitioner _activeTransitioner;

    public void Switch(InputAction.CallbackContext context) => Switch();

    public virtual void Switch()
    {
        gameObject.SetActive(false);
        _switchTo.gameObject.SetActive(true);
        _switchTo.SetActive();
        _onSwitch.Invoke();
    }

    public virtual void SetActive()
    {
        if (_activeTransitioner != null)
            _inputActions.Switch.performed -= _activeTransitioner.Switch;

        _activeTransitioner = this;
        _inputActions.Switch.performed += Switch;
    }
}