using System;
using UnityEngine;

public class SubstanceContainer : MonoBehaviour
{
    [SerializeField] protected Substance _substance;
    public event Action OnSubstanceChanged = delegate { };

    protected virtual void Awake()
    {
        if (Substance == null)
            return;
        
        Substance substance = DataStorage.GetSubstance(Substance);
        Substance = substance;
    }

    public Substance Substance
    {
        get
        {
            if (_substance == null || _substance.Name == null || _substance.Name.Length == 0)
                return null;
            else
                return _substance;
        }
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