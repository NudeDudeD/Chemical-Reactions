using UnityEngine;

[CreateAssetMenu(menuName = "Chemistry/Chemical Reaction")]
public class ChemicalReaction : ScriptableObject
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

    [Header("Mandatory Fields")]
    [SerializeField] private ChemicalSubstance _inputSubstance;
    [SerializeField] private ChemicalSubstance _outputSubstance;

    [Header("Additional Fields")]
    [SerializeField] private ChemicalSubstance _additionalInputSubstance;
    [SerializeField] private ChemicalSubstance _additionalOutputSubstance;
    [SerializeField] private ReactionAgent _agent;
    [SerializeField] private ReactionEffect _effect;
    [SerializeField] private bool _worksInReverse;

    public ChemicalSubstance InputSubstance => _inputSubstance;
    public ChemicalSubstance OutputSubstance => _outputSubstance;
    public ChemicalSubstance AdditionalInputSubstance => _additionalInputSubstance;
    public ChemicalSubstance AdditionalOutputSubstance => _additionalOutputSubstance;
    public ReactionAgent Agent => _agent;
    public ReactionEffect Effect => _effect;
    public bool WorksInReverse => _worksInReverse;

    public ChemicalReaction(ChemicalSubstance inputSubstance, ChemicalSubstance outputSubstance = null, ChemicalSubstance additionalInputSubstance = null, ChemicalSubstance additionalOutputSubstance = null, ReactionAgent reactionAgent = ReactionAgent.None, ReactionEffect reactionEffect = ReactionEffect.None, bool worksInReverse = false)
    {
        _inputSubstance = inputSubstance;
        _outputSubstance = outputSubstance;
        _additionalInputSubstance = additionalInputSubstance;
        _additionalOutputSubstance = additionalOutputSubstance;
        _agent = reactionAgent;
        _effect = reactionEffect;
        _worksInReverse = worksInReverse;
    }
}