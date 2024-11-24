using System;
using UnityEngine;
using UnityEngine.Events;

public class AgentSupplier : MonoBehaviour, IInteractable
{
    [SerializeField] private ReactionHandler _receiver;
    [SerializeField] private Reaction.Agent _agent;
    [SerializeField] private UnityEvent<bool> OnActivityUpdated;
    private string _agentName;
    private bool _isActivated;

    public string Name => (_isActivated ? "Deactivate " : "Activate ") + _agentName;

    private void Awake()
    {
        _agentName = Enum.GetName(typeof(Reaction.Agent), _agent);
    }

    public void Interact(Interactor interactor = null)
    {
        _isActivated = !_isActivated;
        OnActivityUpdated?.Invoke(_isActivated);
        if (_isActivated)
            _receiver.AddAgent(_agent);
        else
            _receiver.RemoveAgent(_agent);
    }
}