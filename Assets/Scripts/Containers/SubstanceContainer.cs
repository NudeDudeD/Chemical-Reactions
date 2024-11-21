using System;
using UnityEngine;

public class SubstanceContainer : MonoBehaviour
{
    [SerializeField] protected Substance _substance;
    public event Action OnSubstanceChanged;

    protected virtual void Awake()
    {
        ChemistryStorage.SubstanceInfo.OnElementRemoved += OnSubstanceRemoved;

        if (_substance == null || _substance.Name == null || _substance.Name.Length == 0)
        {
            Substance = null; 
            return;
        }

        Pair<Substance, MaterialSettings> pair = ChemistryStorage.SubstanceInfo.FindReference(Substance);
        if (pair != null)
            Substance = pair.Key;
        else
            Substance = null;
    }

    public Substance Substance
    {
        get => _substance;

        protected set
        {
            _substance = value;
            OnSubstanceChanged.Invoke();
        }
    }

    protected void OnSubstanceRemoved(Substance substance, MaterialSettings materialSettings)
    {
        if (substance == _substance)
            Substance = null;
    }

    protected void ResetContents() => Substance = null;

    protected virtual void InteractWithContainer(SubstanceContainer interactableContainer) 
    {
        if (Substance == null)
            RequestSubstanceOutput(interactableContainer);
        else
            RequestSubstanceInput(interactableContainer);
    }

    protected virtual void RequestSubstanceInput(SubstanceContainer interactableContainer)
    {
        if (interactableContainer.GetInputRequest(Substance))
            Substance = null;
    }

    protected virtual void RequestSubstanceOutput(SubstanceContainer interactableContainer)
    {
        Substance requestedSubstance = interactableContainer.GetOutputRequest();
        if (requestedSubstance != null)
            Substance = requestedSubstance;
    }

    public virtual bool GetInputRequest(Substance interactorSubstance)
    {
        if (Substance == null)
        {
            Substance = interactorSubstance;
            return true;
        }

        return false;
    } 

    public virtual Substance GetOutputRequest()
    {
        if (Substance == null)
            return null;

        Substance retrievedSubstance = Substance; 
        Substance = null;
        return retrievedSubstance;
    }
}