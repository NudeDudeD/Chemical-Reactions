using UnityEngine;
using UnityEngine.Events;

public class UITransitioner : MonoBehaviour
{
    protected static UITransitioner _activeTransitioner;

    [SerializeField] protected UITransitioner _switchTo;
    [SerializeField] protected bool _showPrevious;
    [SerializeField] protected bool _showOnSwitchedAway;
    [SerializeField] protected UnityEvent _onSwitchedAway;
    [SerializeField] protected UnityEvent _onSwitchedTo;
    [SerializeField] protected UnityEvent _onSwitchedToPrevious;

    protected static void ResetActiveTransitioner(UITransitioner caller, bool showPrevious)
    {
        UIInputHolder.Switch -= _activeTransitioner.Switch;
        _activeTransitioner._onSwitchedAway.Invoke();

        if (caller != _activeTransitioner._switchTo && _activeTransitioner is Popup)
        {
            UITransitioner transitioner = _activeTransitioner._switchTo;
            _activeTransitioner.gameObject.SetActive(false);
            transitioner._onSwitchedAway.Invoke();
            if (!(showPrevious || transitioner._showOnSwitchedAway))
                transitioner.gameObject.SetActive(false);
        }
        else if (!(showPrevious || _activeTransitioner._showOnSwitchedAway))
            _activeTransitioner.gameObject.SetActive(false);
    }

    public virtual void Switch()
    {
        _switchTo.SetAsActiveTransitioner();
        _onSwitchedToPrevious.Invoke();
    }

    public virtual void SetAsActiveTransitioner()
    {
        if (_activeTransitioner != null)
            ResetActiveTransitioner(this, _showPrevious);

        UIInputHolder.Switch += Switch;
        gameObject.SetActive(true);
        _activeTransitioner = this;
        _onSwitchedTo.Invoke();
    }
}