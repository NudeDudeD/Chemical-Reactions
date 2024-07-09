using System;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalReactionHandler : MonoBehaviour
{
    [SerializeField] private List<ChemicalReaction> _reactions;
    [SerializeField] private SubstanceContainer _container;
    [SerializeField] private SubstanceContainer _additionalContainer;
    
    private ChemicalReaction _latestReaction;
    private ChemicalReaction.ReactionAgent _agent;
    private bool _inProcess;

    public ChemicalReaction LatestReaction
    {
        get => _latestReaction;
        set
        {
            _latestReaction = value;
            OnReactionPerformed.Invoke();
        }
    }

    public ChemicalReaction.ReactionAgent Agent
    {
        get => _agent;
        set
        {
            _agent = value;
            OnAgentChanged.Invoke();
        }
    }

    public event Action OnReactionPerformed = delegate { };
    public event Action OnAgentChanged = delegate { };

    private void Awake()
    {
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
            ChemicalSubstance substance = _additionalContainer.GetOutputRequest();
            if (substance != null)
                _container.GetInputRequest(substance);
        }
    }

    private void TryReact() //Big 'if' hell begins here...
    {
        if (_inProcess)
            return;

        ChemicalSubstance outputSubstance;
        ChemicalSubstance additionalOutputSubstance;

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

    private bool TryFindReaction(out ChemicalSubstance outputSubstance, out ChemicalSubstance additionalOutputSubstance)
    {
        bool worksInReverse = false;
        bool switchedSubstances = false;
        outputSubstance = null;
        additionalOutputSubstance = null;

        List<ChemicalReaction> reactions = _reactions.FindAll(reaction => reaction.Agent == ChemicalReaction.ReactionAgent.None || reaction.Agent == _agent);

        if (reactions.Count == 0)
            return false;

        ChemicalReaction reaction;

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

    private bool ChemicalComparerSwitching(ChemicalSubstance reactionSubstance, ChemicalSubstance comparable, out bool switchedSubstances, ChemicalSubstance additionalReactionSubstance = null, ChemicalSubstance additionalComparable = null)
    {
        switchedSubstances = false;

        if (ChemicalsComparer(reactionSubstance, comparable, additionalReactionSubstance, additionalComparable))
            return true;
        if (ChemicalsComparer(additionalReactionSubstance, comparable, reactionSubstance, additionalComparable))
        {
            switchedSubstances = true;
            return true;
        }

        return false;
    }

    private bool ChemicalsComparer(ChemicalSubstance reactionSubstance, ChemicalSubstance comparable, ChemicalSubstance additionalReactionSubstance = null, ChemicalSubstance additionalComparable = null)
        => reactionSubstance == comparable && additionalReactionSubstance == additionalComparable;
}