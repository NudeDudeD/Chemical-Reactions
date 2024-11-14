using System;
using UnityEngine;

public class HeatEmitter : MonoBehaviour, ISimpleInteractable
{
    [SerializeField] private ReactionHandler _receiver;
    private bool _isActivated;

    public event Action<bool> OnActivitySwitch = delegate { };

    public void Interact(SimpleInteractor interactor = null)
    {
        _isActivated = !_isActivated;
        OnActivitySwitch.Invoke(_isActivated);
        if (_isActivated)
            _receiver.AddAgent(Reaction.Agent.Heat);
        else
            _receiver.RemoveAgent(Reaction.Agent.Heat);
    }
}