using UnityEngine;

[CreateAssetMenu(menuName = "Chemistry/ChemicalReaction")]
public class ChemicalReaction : ScriptableObject
{
    [Header("Mandatory Fields")]
    [SerializeField] private ChemicalSubstance _inputElement;
    [SerializeField] private ChemicalSubstance _outputElement;

    [Header("Additional Fields")]
    [SerializeField] private ChemicalSubstance _additionalInputElement;
    [SerializeField] private ChemicalSubstance _additionalOutputElement;
    [SerializeField] private ReactionAgent _reactionAgent;
    [SerializeField] private ReactionEffect _reactionEffect;

    public ChemicalSubstance InputElement => _inputElement;
    public ChemicalSubstance OutputElement => _outputElement;
    public ChemicalSubstance AdditionalInputElement => _additionalInputElement;
    public ChemicalSubstance AdditionalOutputElement => _additionalOutputElement;
    public ReactionAgent ReactionAgent => _reactionAgent;
    public ReactionEffect ReactionEffect => _reactionEffect;

    public ChemicalReaction(ChemicalSubstance requiredElement, ChemicalSubstance outputElement, ChemicalSubstance additionalElement = null, ChemicalSubstance additionalOutputElement = null, ReactionAgent reactionAgent = ReactionAgent.None, ReactionEffect reactionEffect = ReactionEffect.None)
    {
        _inputElement = requiredElement;
        _outputElement = outputElement;
        _additionalInputElement = additionalElement;
        _additionalOutputElement = additionalOutputElement;
        _reactionAgent = reactionAgent;
        _reactionEffect = reactionEffect;
    }
}