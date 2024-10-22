using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
    
public class UITransitioner : MonoBehaviour
{
    protected static UITransitioner _activeTransitioner;

    [SerializeField] protected UIInputHolder _inputActions;
    [SerializeField] protected UITransitioner _switchTo;
    [SerializeField] protected UnityEvent _onSwitch;
    [SerializeField] protected bool _showOnSwitch;

    public void Switch(InputAction.CallbackContext context) => Switch();

    public virtual void SetAsActiveTransitioner()
    {
        if (_activeTransitioner != null)
        {
            _inputActions.Switch.performed -= _activeTransitioner.Switch;
            if (!_activeTransitioner._showOnSwitch)
                _activeTransitioner.gameObject.SetActive(false);
        }
        
        _inputActions.Switch.performed += Switch;
        gameObject.SetActive(true);
        _activeTransitioner = this;
    }

    public virtual void Switch()
    {
        if (!_showOnSwitch)
            gameObject.SetActive(false);
        _switchTo.gameObject.SetActive(true);
        _switchTo.SetAsActiveTransitioner();
        _onSwitch.Invoke();
    }
}