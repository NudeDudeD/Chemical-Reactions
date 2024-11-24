using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class SubstanceMeshVisualiser : SubstanceVisualizer
{
    private Renderer _substanceRenderer;

    protected override void Awake()
    {
        base.Awake();
        _substanceRenderer = GetComponent<Renderer>();
        Container.OnSubstanceChanged += (_) => Revisualize();
    }

    protected override void Revisualize()
    {
        Substance substance = Container.Substance;
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