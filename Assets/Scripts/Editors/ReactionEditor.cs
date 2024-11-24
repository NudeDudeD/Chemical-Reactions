using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReactionEditor : MonoBehaviour
{
    [SerializeField] private SubstanceIconList _substanceIconList;
    [SerializeField] private SubstanceIcon _reactiveIcon;
    [SerializeField] private SubstanceIcon _additionalReactiveIcon;
    [SerializeField] private SubstanceIcon _productIcon;
    [SerializeField] private SubstanceIcon _additionalProductIcon;
    [SerializeField] private UnityEvent OnCreated;
    private List<Reaction.Agent> _agents = new List<Reaction.Agent>();
    private SubstanceIcon _selectedIcon;
    private Reaction.VisualEffect _effect;
    private bool _worksInReverse;

    private void Awake()
    {
        SubstanceIcon[] substanceIcons = new SubstanceIcon[] { _reactiveIcon, _additionalReactiveIcon, _productIcon, _additionalProductIcon };
        for (int i = 0; i < substanceIcons.Length; i++)
        {
            SubstanceIcon eventIcon = substanceIcons[i];
            eventIcon.OnSelect += (_) => _selectedIcon = eventIcon;
            eventIcon.OnDeselect += (_) =>
            {
                if (_selectedIcon == eventIcon)
                    _selectedIcon = null;
            };
            for (int j = 0; j < substanceIcons.Length; j++)
                if (j != i)
                {
                    SubstanceIcon disablingIcon = substanceIcons[j];
                    eventIcon.OnSelect += (_) => disablingIcon.Selected = false;
                }
        }

        ResetParameters();
    }

    private void OnDisable() => ResetParameters();

    private void ResetParameters()
    {
        SubstanceIcon[] substanceIcons = new SubstanceIcon[] { _reactiveIcon, _additionalReactiveIcon, _productIcon, _additionalProductIcon };
        if (_selectedIcon != null)
            _selectedIcon.Selected = false;
        
        foreach (SubstanceIcon icon in substanceIcons)
            icon.Substance = null;
    }

    public void Create()
    {
        if (_reactiveIcon.Substance == null && _additionalReactiveIcon.Substance == null)
        {
            MessageBox.Show("Error", "At least one reactive should be added.");
            return;
        }

        Reaction reaction = new Reaction(_reactiveIcon.Substance, _additionalReactiveIcon.Substance, _productIcon.Substance, _additionalProductIcon.Substance, _agents.ToArray(), _effect, _worksInReverse);
        if (!ChemistryStorage.Reactions.Add(reaction))
        {
            MessageBox.Show("Error", "Error creating a reaction.");
            return;
        }

        OnCreated.Invoke();
    }

    public void Fill()
    {
        Substance substance = _substanceIconList.GetLastSelected();
        if (substance == null || _selectedIcon == null)
        {
            MessageBox.Show("Information", "Selection is missing. Select the substance and the reactive/product icon, then press the button to fill the icon.");
            return;
        }
        _selectedIcon.Substance = substance;
    }

    public void SetEffect(int effect) => _effect = (Reaction.VisualEffect)effect;

    public void SetReversibility(bool worksInReverse) => _worksInReverse = worksInReverse;

    public void UpdateHeatAgent(bool add)
    {
        if (add)
            AddAgent(Reaction.Agent.Heat);
        else
            RemoveAgent(Reaction.Agent.Heat);
    }

    public void UpdateLightAgent(bool add)
    {
        if (add)
            AddAgent(Reaction.Agent.Light);
        else
            RemoveAgent(Reaction.Agent.Light);
    }

    public void UpdateElectricityAgent(bool add)
    {
        if (add)
            AddAgent(Reaction.Agent.Electricity);
        else
            RemoveAgent(Reaction.Agent.Electricity);
    }

    public void AddAgent(Reaction.Agent agent)
    {
        if (_agents.Exists(a => a == agent))
            return;
        _agents.Add(agent);
    }

    public void RemoveAgent(Reaction.Agent agent)
    {
        if (_agents.Exists(a => a != agent))
            return;
        _agents.Remove(agent);
    }
}