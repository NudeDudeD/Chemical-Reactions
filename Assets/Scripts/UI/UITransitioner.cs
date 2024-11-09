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

    public virtual void Switch() => _switchTo.SetAsActiveTransitioner();

    public virtual void SetAsActiveTransitioner()
    {
        if (_activeTransitioner != null)
        {
            UIInputHolder.Switch -= _activeTransitioner.Switch;
            if (!(_showPrevious || _activeTransitioner._showOnSwitchedAway))
                _activeTransitioner.gameObject.SetActive(false);
            _activeTransitioner._onSwitchedAway.Invoke();
        }

        UIInputHolder.Switch += Switch;
        gameObject.SetActive(true);
        _activeTransitioner = this;
        _onSwitchedTo.Invoke();
    }
}