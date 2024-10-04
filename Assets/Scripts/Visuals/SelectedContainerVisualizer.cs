using UnityEngine;
using TMPro;

public class SelectedContainerVisualizer : SubstanceVisualizer
{
    [SerializeField] private ObjectSelector _selector;
    private TMP_Text _tmpText;

    private void Awake()
    {
        _tmpText = GetComponent<TMP_Text>();
        _selector.OnObjectChanged += UpdateObject;
    }

    protected override void Revisualize()
    {
        string text;

        if (_container == null)
            return;

        if (_container.Substance == null)
            text = "<Container is empty>";
        else
            text = "[" + _container.Substance.Name + "]";

        _tmpText.text = text;
    }

    private void ResetText() => _tmpText.text = string.Empty;

    private void UpdateObject()
    {
        if (_selector.TryGetSelectedComponent(out SubstanceContainer container))
        {
            if (_container != null)
                _container.OnSubstanceChanged -= Revisualize;

            _container = container;
            _container.OnSubstanceChanged += Revisualize;

            Revisualize();
        }
        else if (_container != null)
        {
            _container.OnSubstanceChanged -= Revisualize;
            _container = null;

            ResetText();
        }
    }
}