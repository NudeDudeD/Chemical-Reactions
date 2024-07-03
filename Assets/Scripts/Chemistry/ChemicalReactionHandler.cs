using System.Collections.Generic;
using UnityEngine;

public class ChemicalReactionHandler : MonoBehaviour
{
    private List<ChemicalSubstance> _chemicalElements;
    [SerializeField] private List<ChemicalReaction> _reactions;

    private void Start()
    {
        _chemicalElements = new List<ChemicalSubstance>();
        
    }



    public void ElementInput(ChemicalSubstance element)
    {
        _chemicalElements.Add(element);
    }
}