using System.Linq;

public class SubstanceIconList : IconList
{
    protected override string NoSelectedMessage => "No substance was selected. To remove substances, select their icons and then press Remove button.";

    protected override string DeletionMessage => "The following substances (and ALL reactions with them!) will be deleted:";

    private void Start()
    {
        for (int i = 0; i < ChemistryStorage.SubstanceInfo.Count; i++)
        {
            Pair<Substance, MaterialSettings> pair = ChemistryStorage.SubstanceInfo[i];
            AddIcon(pair.Key, pair.Value);
        }

        ChemistryStorage.SubstanceInfo.OnElementAdded += AddIcon;
        ChemistryStorage.SubstanceInfo.OnElementRemoved += (Substance substance, MaterialSettings materialSettings) => RemoveIcon(substance.Name);
    }

    private void AddIcon(Substance substance, MaterialSettings materialSettings)
    {
        SubstanceIcon icon = Instantiate((SubstanceIcon)_iconPrefab);
        icon.Substance = substance;
        AddIcon(icon);
    }

    protected override void RemoveSelected()
    {
        for (int i = _selectedIcons.Count - 1; i >= 0; i--)
        {
            SubstanceIcon icon = (SubstanceIcon)_selectedIcons[i];
            icon.Selected = false;
            ChemistryStorage.SubstanceInfo.Remove(icon.Substance);
        }
    }

    //i had an idea to use button event to fill syringe (addinputrequest) with output of a getselected, but that ain't working bro
    public void FillWithSelected(SubstanceContainer container)
    {
        if (!container.GetInputRequest(GetLastSelected()))
            MessageBox.Show("Information", "Container is already filled with " + container.Substance.Name + ".\nEmpty your container before trying to fill it.");
    }

    public Substance GetLastSelected() => _selectedIcons.Count == 0 ? null : ((SubstanceIcon)_selectedIcons.Last()).Substance;
}