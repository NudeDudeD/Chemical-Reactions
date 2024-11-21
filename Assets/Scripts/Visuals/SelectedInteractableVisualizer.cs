using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class SelectedInteractableVisualizer : MonoBehaviour
{
    [SerializeField] ObjectSelector _selector;
    [SerializeField] Interactor _interactor;

    private TMP_Text _tmpText;

    private void Awake()
    {
        _tmpText = GetComponent<TMP_Text>();
        _selector.OnObjectChanged += Revisualize;
        _interactor.OnInteraction += Revisualize;
    }

    private void Revisualize() => _tmpText.text = _selector.TryGetSelectedComponent(out IInteractable simpleInteractable) ? "[E] " + simpleInteractable.Name : string.Empty;
}