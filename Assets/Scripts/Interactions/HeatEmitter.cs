using System;
using UnityEngine;

public class HeatEmitter : MonoBehaviour, ISimpleInteractable
{
    [SerializeField] private ChemicalReactionHandler _receiver;
    private bool _isActivated;

    public event Action OnActiveSwitch = delegate { };

    public void Interact(SimpleInteractor interactor = null)
    {
        _isActivated = !_isActivated;
        OnActiveSwitch.Invoke();
        _receiver.Agent = _isActivated ? ChemicalReaction.ReactionAgent.Heat : ChemicalReaction.ReactionAgent.None;
    }
}