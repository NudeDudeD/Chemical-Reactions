using UnityEngine;

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
        ChemicalSubstance currentSubstance = _container.Substance;
        if (currentSubstance == null)
        {
            _substanceRenderer.enabled = false;
        }
        else
        {
            _substanceRenderer.enabled = true;
            _substanceRenderer.sharedMaterial = currentSubstance.Material;
        }
    }
}