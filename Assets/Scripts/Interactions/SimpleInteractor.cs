using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ObjectSelector), typeof(PlayerInputHolder))]
public class SimpleInteractor : MonoBehaviour
{
    private ObjectSelector _selector;

    private void Awake()
    {
        _selector = GetComponent<ObjectSelector>();
    }

    private void Start()
    {
        PlayerInputHolder inputManager = GetComponent<PlayerInputHolder>();
        inputManager.InteractWithObject.performed += Interact;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (_selector.TryGetSelectedComponent(out ISimpleInteractable simpleInteractable))
            simpleInteractable.Interact();
    }
}
