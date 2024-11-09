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

        Pair<Substance, MaterialSettings> pair = ChemistryStorage.SubstanceInfo.Find(Substance);
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