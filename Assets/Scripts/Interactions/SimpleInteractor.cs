using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ObjectSelector))]
public class SimpleInteractor : MonoBehaviour
{
    private ObjectSelector _selector;

    private void Awake()
    {
        _selector = GetComponent<ObjectSelector>();
    }

    private void Start()
    {
        PlayerInputHolder.InteractWithObject += Interact;
    }

    private void Interact()
    {
        if (_selector.TryGetSelectedComponent(out ISimpleInteractable simpleInteractable))
            simpleInteractable.Interact();
    }
}
