using System;
using UnityEngine;

public class SubstanceContainer : MonoBehaviour
{
    [SerializeField] protected ChemicalSubstance _substance;
    public event Action OnSubstanceChanged = delegate { };

    public ChemicalSubstance Substance
    {
        get => _substance;
        protected set
        {
            _substance = value;
            OnSubstanceChanged.Invoke();
        }
    }

    protected virtual void InteractWithContainer(ref SubstanceContainer interactableContainer) 
    {
        if (Substance == null)
            RequestSubstanceOutput(ref interactableContainer);
        else
            RequestSubstanceInput(ref interactableContainer);
    }

    protected virtual void RequestSubstanceInput(ref SubstanceContainer interactableContainer)
    {
        if (interactableContainer.GetInputRequest(Substance))
            Substance = null;
    }

    protected virtual void RequestSubstanceOutput(ref SubstanceContainer interactableContainer)
    {
        ChemicalSubstance requestedSubstance = interactableContainer.GetOutputRequest();
        if (requestedSubstance != null)
            Substance = requestedSubstance;
    }

    public virtual bool GetInputRequest(ChemicalSubstance interactorSubstance)
    {
        if (Substance == null)
        {
            Substance = interactorSubstance;
            return true;
        }
        else
            return false;
    }

    public virtual ChemicalSubstance GetOutputRequest()
    {
        if (Substance == null)
            return null;

        ChemicalSubstance retrievedSubstance = Substance; 
        Substance = null;
        return retrievedSubstance;
    }
}