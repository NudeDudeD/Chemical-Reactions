using System.Collections;
using System.Linq;
using UnityEngine;

public class SubstanceIconList : IconList
{
    [SerializeField] private SubstanceIcon _iconPrefab;

    private void Start()
    {
        foreach (Pair<Substance, MaterialSettings> pair in ChemistryStorage.SubstanceInfo.List)
            AddIcon(pair.Key, pair.Value);

        ChemistryStorage.SubstanceInfo.OnElementAdded += AddIcon;
        ChemistryStorage.SubstanceInfo.OnElementRemoved += RemoveIcon;
    }

    private void RemoveIcon(Substance substance, MaterialSettings materialSettings) => RemoveIcon(substance.Name);

    private void AddIcon(Substance substance, MaterialSettings materialSettings)
    {
        SubstanceIcon icon = Instantiate(_iconPrefab);
        icon.Substance = substance;
        AddIcon(icon);
    }

    private void RemoveSelected(bool callback)
    {
        if (!callback)
            return;

        for (int i = _selectedIcons.Count - 1; i >= 0; i--)
        {
            SubstanceIcon icon = (SubstanceIcon)_selectedIcons[i];
            icon.Selected = false;
            ChemistryStorage.SubstanceInfo.Remove(icon.Substance);
            RemoveIcon(icon);
            icon.gameObject.SetActive(false);
        }
    }

    public void RequestRemoveSelected()
    {
        if (_selectedIcons.Count == 0)
        {
            MessageBox.Show("Information", "No substance was selected. To remove a substance, select their icons and then press Remove button.");
            return;
        }

        string message = "The following substances will be deleted:";
        foreach (SubstanceIcon icon in _selectedIcons.Cast<SubstanceIcon>())
            message += '\n' + icon.Substance.Name;
        MessageBox.Show("Substances", message, RemoveSelected, MessageBox.Buttons.OkCancel);
    }
}