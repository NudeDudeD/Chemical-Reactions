using UnityEngine;

public class ControllableSubstanceContainer : SubstanceContainer
{
    [SerializeField] private ObjectSelector _selector;

    protected override void Awake()
    {
        base.Awake();
        PlayerInputHolder.InteractWithObject += TryInteractWithContainer;
    }

    private void TryInteractWithContainer()
    {
        if (_selector.TryGetSelectedComponent(out SubstanceContainer container))
            InteractWithContainer(ref container);
    }
}