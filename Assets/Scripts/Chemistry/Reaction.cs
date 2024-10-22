using System;
using UnityEngine;

[System.Serializable]
public class Reaction : IComparable<Reaction>
{
    public enum ReactionAgent
    {
        None,
        Heat
    }

    public enum ReactionEffect
    {
        None,
        Explosion
    }

    [SerializeField] private Substance _inputSubstance;
    [SerializeField] private Substance _outputSubstance;
    [SerializeField] private Substance _additionalInputSubstance;
    [SerializeField] private Substance _additionalOutputSubstance;
    [SerializeField] private ReactionAgent _agent;
    [SerializeField] private ReactionEffect _effect;
    [SerializeField] private bool _worksInReverse;

    public Substance InputSubstance => _inputSubstance;
    public Substance OutputSubstance => _outputSubstance;
    public Substance AdditionalInputSubstance => _additionalInputSubstance;
    public Substance AdditionalOutputSubstance => _additionalOutputSubstance;
    public ReactionAgent Agent => _agent;
    public ReactionEffect Effect => _effect;
    public bool WorksInReverse => _worksInReverse;

    public Reaction(Substance inputSubstance, Substance outputSubstance = null, Substance additionalInputSubstance = null, Substance additionalOutputSubstance = null, ReactionAgent reactionAgent = ReactionAgent.None, ReactionEffect reactionEffect = ReactionEffect.None, bool worksInReverse = false)
    {
        _inputSubstance = inputSubstance;
        _outputSubstance = outputSubstance;
        _additionalInputSubstance = additionalInputSubstance;
        _additionalOutputSubstance = additionalOutputSubstance;
        _agent = reactionAgent;
        _effect = reactionEffect;
        _worksInReverse = worksInReverse;
    }

    public int CompareTo(Reaction other)
    {
        //Coming sometime
        return 0;
    }
}