using System;
using UnityEngine;

[RequireComponent(typeof(ObjectSelector))]
public class Interactor : MonoBehaviour
{
    private ObjectSelector _selector;

    public event Action OnInteraction = delegate { };

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
        if (_selector.TryGetSelectedComponent(out IInteractable simpleInteractable))
        {
            simpleInteractable.Interact();
            OnInteraction.Invoke();
        }
    }
}
