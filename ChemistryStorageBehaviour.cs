using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ChemicalReaction;

public class ChemistryStorageBehaviour : MonoBehaviour
{
    private static List<ChemicalSubstance> _chemicalSubstances = new List<ChemicalSubstance>();
    public static List<ChemicalSubstance> ChemicalSubstances => _chemicalSubstances;

    [SerializeField] private ControllableSubstanceContainer _controllableSubstanceContainer;
    [SerializeField] private InputField _substanceNameInput;
    [SerializeField] private InputField _substanceMaterialRedInput;
    [SerializeField] private InputField _substanceMaterialGreenInput;
    [SerializeField] private InputField _substanceMaterialBlueInput;

    [SerializeField] private Dropdown _dropdown;
    [SerializeField] private ChemicalSubstance _inputSubstance;
    [SerializeField] private ChemicalSubstance _outputSubstance;
    [SerializeField] private ChemicalSubstance _additionalInputSubstance;
    [SerializeField] private ChemicalSubstance _additionalOutputSubstance;
    [SerializeField] private ReactionAgent _agent;
    [SerializeField] private ReactionEffect _effect;
    [SerializeField] private bool _worksInReverse;

    public void AddSubstance()
    {
        Material material = new Material(Shader.Find("Standard"));
        material.color = new Color(float.Parse(_substanceMaterialRedInput.text),float.Parse(_substanceMaterialGreenInput.text),float.Parse(_substanceMaterialBlueInput.text));
        ChemicalSubstance substance = new ChemicalSubstance(_substanceNameInput.text, material, ChemicalSubstance.MatterState.Liquid);
        _controllableSubstanceContainer.GetOutputRequest();
        _controllableSubstanceContainer.GetInputRequest(substance);
    }
}