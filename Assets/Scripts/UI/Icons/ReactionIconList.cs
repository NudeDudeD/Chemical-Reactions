
using UnityEngine;

public class ReactionIconList : IconList //repeats code from SubstaneIconList, no time to figure this out
{
    protected override string NoSelectedMessage => "No reactions was selected. To remove reactions, select their icons and then press Remove button.";

    protected override string DeletionMessage => "The following reactions will be deleted:";

    private void Start()
    {
        for (int i = 0; i < ChemistryStorage.Reactions.Count; i++)
        {
            Reaction reaction = ChemistryStorage.Reactions[i];
            AddIcon(reaction);
        }

        ChemistryStorage.Reactions.OnElementAdded += AddIcon;
        ChemistryStorage.Reactions.OnElementRemoved += (reaction) => RemoveIcon(reaction.Name);
    }

    private void AddIcon(Reaction reaction)
    {
        ReactionIcon icon = Instantiate((ReactionIcon)_iconPrefab);
        icon.Reaction = reaction;
        AddIcon(icon);
    }

    protected override void RemoveSelected()
    {
        for (int i = _selectedIcons.Count - 1; i >= 0; i--)
        {
            ReactionIcon icon = (ReactionIcon)_selectedIcons[i];
            icon.Selected = false;
            ChemistryStorage.Reactions.Remove(icon.Reaction);
        }
    }
}