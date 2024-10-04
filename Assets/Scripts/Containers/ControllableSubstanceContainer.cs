using UnityEngine;
using UnityEngine.InputSystem;

public class ControllableSubstanceContainer : SubstanceContainer
{
    [SerializeField] private PlayerInputHolder _inputHolder;
    [SerializeField] private ObjectSelector _selector;

    protected override void Awake()
    {
        base.Awake();
        _inputHolder.InteractWithObject.performed += TryInteractWithContainer;
    }

    private void TryInteractWithContainer(InputAction.CallbackContext context)
    {
        if (_selector.TryGetSelectedComponent(out SubstanceContainer container))
            InteractWithContainer(ref container);
    }
}