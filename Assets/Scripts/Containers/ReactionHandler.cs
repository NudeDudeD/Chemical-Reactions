﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class ReactionHandler : MonoBehaviour
{
    [SerializeField] private SubstanceContainer _container;
    [SerializeField] private SubstanceContainer _additionalContainer;
    private List<Reaction.Agent> _agents = new List<Reaction.Agent>();

    public event Action<Reaction> OnReactionPerformed = delegate { };
    public event Action OnAgentsChanged = delegate { };

    private bool _isReacting = false;

    private void Awake()
    {
        _container.OnSubstanceChanged += FillCheck;
        _additionalContainer.OnSubstanceChanged += FillCheck;
        _container.OnSubstanceChanged += TryReact;
        _additionalContainer.OnSubstanceChanged += TryReact;
        OnAgentsChanged += TryReact;
    }

    private void FillCheck()
    {
        if (_container.Substance == null && _additionalContainer != null)
        {
            Substance substance = _additionalContainer.GetOutputRequest();
            if (substance != null)
                _container.GetInputRequest(substance);
        }
    }

    private void TryReact()
    {
        if (_isReacting)
            return;

        _isReacting = true;
        bool inverted = false;
        Reaction.Agent[] agents = _agents.ToArray();
        Reaction reaction = ChemistryStorage.Reactions.Find(r => r.CanReact(_container.Substance, _additionalContainer.Substance, agents, out inverted));

        if (reaction == null)
        {
            _isReacting = false;
            return;
        }

        Substance product = inverted ? reaction.Reactive : reaction.Product;
        Substance additionalProduct = inverted ? reaction.AdditionalReactive : reaction.AdditionalProduct;

        _additionalContainer.GetOutputRequest();
        _container.GetOutputRequest();
        if (!_container.GetInputRequest(product))
            _additionalContainer.GetInputRequest(product);
        else if (additionalProduct != null)
            _additionalContainer.GetInputRequest(additionalProduct);

        OnReactionPerformed.Invoke(reaction);
        _isReacting = false;
    }

    public void AddAgent(Reaction.Agent agent)
    {
        if (_agents.Exists(a => a == agent))
            return;

        _agents.Add(agent);
        OnAgentsChanged.Invoke();
    }

    public void RemoveAgent(Reaction.Agent agent)
    {
        bool deleted = _agents.Remove(agent);
        if (!deleted)
            return;

        OnAgentsChanged.Invoke();
    }
}