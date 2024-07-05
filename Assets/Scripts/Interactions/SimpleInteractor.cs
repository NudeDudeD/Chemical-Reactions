using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ObjectSelector), typeof(InputManager))]
public class SimpleInteractor : MonoBehaviour
{
    private ObjectSelector _selector;

    private void Awake()
    {
        _selector = GetComponent<ObjectSelector>();
    }

    private void Start()
    {
        InputManager inputManager = GetComponent<InputManager>();
        inputManager.InteractWithObject.performed += Interact;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (_selector.TryGetSelectedComponent(out ISimpleInteractable simpleInteractable))
            simpleInteractable.Interact();
    }
}
