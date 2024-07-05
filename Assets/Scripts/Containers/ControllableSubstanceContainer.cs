using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(InputManager))]
public class ControllableSubstanceContainer : SubstanceContainer
{
    [SerializeField] private ObjectSelector _selector;

    private void Start()
    {
        InputManager inputManager = GetComponent<InputManager>();
        inputManager.InteractWithObject.performed += Interact;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (_selector.TryGetSelectedComponent(out SubstanceContainer container))
            InteractWithContainer(ref container);
    }
}