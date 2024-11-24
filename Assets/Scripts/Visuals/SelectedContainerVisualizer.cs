using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class SelectedContainerVisualizer : SubstanceVisualizer
{
    [SerializeField] private ObjectSelector _selector;
    private TMP_Text _tmpText;

    protected override void Awake()
    {
        base.Awake();
        _tmpText = GetComponent<TMP_Text>();
        _selector.OnObjectChanged += UpdateObject;
    }

    protected override void Revisualize()
    {
        if (Container == null)
        {
            _tmpText.text = string.Empty;
            return;
        }

        Substance substance = Container.Substance;
        string text;

        if (Container == null)
            return;

        if (substance == null)
            text = "[F] <Container is empty>";
        else
            text = "[F] {" + substance.Name + "}";

        _tmpText.text = text;
    }

    private void UpdateObject()
    {
        Container = _selector.TryGetSelectedComponent(out SubstanceContainer container) ? container : null;
    }
}