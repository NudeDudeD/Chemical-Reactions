using System;
using System.Collections.Generic;
using UnityEngine;

public class ReactionHandler : MonoBehaviour
{
    [SerializeField] private SubstanceContainer _container;
    [SerializeField] private SubstanceContainer _additionalContainer;
    private List<Reaction> _reactions;
    private bool _inProcess;
    private Reaction _latestReaction;
    private Reaction.ReactionAgent _agent;

    public Reaction LatestReaction
    {
        get => _latestReaction;
        private set
        {
            _latestReaction = value;
            OnReactionPerformed.Invoke();
        }
    }

    public Reaction.ReactionAgent Agent
    {
        get => _agent;
        set
        {
            _agent = value;
            OnAgentChanged.Invoke();
        }
    }

    public bool InProcess => _inProcess;

    public event Action OnReactionPerformed = delegate { };
    public event Action OnAgentChanged = delegate { };

    private void Awake()
    {
        _reactions = new List<Reaction>();
        _container.OnSubstanceChanged += FillCheck;
        _additionalContainer.OnSubstanceChanged += FillCheck;
        _container.OnSubstanceChanged += TryReact;
        _additionalContainer.OnSubstanceChanged += TryReact;
        OnAgentChanged += TryReact;
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

    private void TryReact() //Big 'if' hell begins here...
    {
        if (_reactions.Count == 0)
            return;

        if (_inProcess)
            return;

        Substance outputSubstance;
        Substance additionalOutputSubstance;

        _inProcess = true;
        if (!TryFindReaction(out outputSubstance, out additionalOutputSubstance))
        {
            _inProcess = false;
            return;
        }           

        _additionalContainer.GetOutputRequest();
        _container.GetOutputRequest();
        if (!_container.GetInputRequest(outputSubstance))
            _additionalContainer.GetInputRequest(outputSubstance);
        else if (additionalOutputSubstance != null)
            _additionalContainer.GetInputRequest(additionalOutputSubstance);

        _inProcess = false;
    }

    private bool TryFindReaction(out Substance outputSubstance, out Substance additionalOutputSubstance)
    {
        bool worksInReverse = false;
        bool switchedSubstances = false;
        outputSubstance = null;
        additionalOutputSubstance = null;

        List<Reaction> reactions = _reactions.FindAll(reaction => reaction.Agent == Reaction.ReactionAgent.None || reaction.Agent == _agent);

        if (reactions.Count == 0)
            return false;

        Reaction reaction;

        reaction = reactions.Find(listReaction => ChemicalComparerSwitching(listReaction.InputSubstance, _container.Substance, out switchedSubstances, listReaction.AdditionalInputSubstance, _additionalContainer.Substance));

        if (reaction == null)
        {
            reactions = reactions.FindAll(reaction => reaction.WorksInReverse);
            if (reactions.Count == 0)
                return false;
            reaction = reactions.Find(listReaction => ChemicalComparerSwitching(listReaction.OutputSubstance, _container.Substance, out switchedSubstances, listReaction.AdditionalOutputSubstance, _additionalContainer.Substance));
            if (reaction == null)
                return false;
            else
                worksInReverse = true;
        }

        if (switchedSubstances)
        {
            outputSubstance = worksInReverse ? reaction.AdditionalInputSubstance : reaction.AdditionalOutputSubstance;
            additionalOutputSubstance = worksInReverse ? reaction.InputSubstance : reaction.OutputSubstance;
        }
        else
        {
            outputSubstance = worksInReverse ? reaction.InputSubstance : reaction.OutputSubstance;
            additionalOutputSubstance = worksInReverse ? reaction.AdditionalInputSubstance : reaction.AdditionalOutputSubstance;
        }

        LatestReaction = reaction;
        return true;
    }

    private bool ChemicalComparerSwitching(Substance reactive, Substance comparable, out bool switchedSubstances, Substance additionalReactive = null, Substance additionalComparable = null)
    {
        switchedSubstances = false;

        if (ChemicalsComparer(reactive, comparable, additionalReactive, additionalComparable))
            return true;
        if (ChemicalsComparer(additionalReactive, comparable, reactive, additionalComparable))
        {
            switchedSubstances = true;
            return true;
        }

        return false;
    }

    private bool ChemicalsComparer(Substance reactive, Substance comparable, Substance additionalReactive = null, Substance additionalComparable = null)
        => reactive == comparable && additionalReactive == additionalComparable;
}