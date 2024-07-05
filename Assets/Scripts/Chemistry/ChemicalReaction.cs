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

    public ChemicalReaction(ChemicalSubstance requiredSubstance, ChemicalSubstance outputSubstance = null, ChemicalSubstance additionalSubstance = null, ChemicalSubstance additionalOutputSubstance = null, ReactionAgent reactionAgent = ReactionAgent.None, ReactionEffect reactionEffect = ReactionEffect.None, bool worksInReverse = false)
    {
        _inputSubstance = requiredSubstance;
        _outputSubstance = outputSubstance;
        _additionalInputSubstance = additionalSubstance;
        _additionalOutputSubstance = additionalOutputSubstance;
        _agent = reactionAgent;
        _effect = reactionEffect;
        _worksInReverse = worksInReverse;
    }
}