using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class SubstanceMeshVisualiser : SubstanceVisualizer
{
    private Renderer _substanceRenderer;

    private void Awake()
    {
        _substanceRenderer = GetComponent<Renderer>();
        _container.OnSubstanceChanged += Revisualize;
    }

    protected override void Revisualize()
    {
        Substance substance = _container.Substance;
        if (substance == null)
        {
            _substanceRenderer.enabled = false;
        }
        else
        {
            _substanceRenderer.enabled = true;
            _substanceRenderer.material = ChemistryStorage.SubstanceInfo.FindValue(substance)?.Material;
        }
    }
}