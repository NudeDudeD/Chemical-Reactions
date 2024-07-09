using UnityEngine;
using UnityEngine.InputSystem;

public class ControllableSubstanceContainer : SubstanceContainer
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private ObjectSelector _selector;

    private void Start()
    {
        _inputManager.InteractWithObject.performed += TryInteractWithContainer;
    }

    private void TryInteractWithContainer(InputAction.CallbackContext context)
    {
        if (_selector.TryGetSelectedComponent(out SubstanceContainer container))
            InteractWithContainer(ref container);
    }
}