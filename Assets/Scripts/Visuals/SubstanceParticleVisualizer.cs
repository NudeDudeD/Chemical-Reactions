using UnityEngine;

public class SubstanceParticleVisualizer : SubstanceVisualizer
{
    private ParticleSystem _particleSystem;
    private ParticleSystemRenderer _particleRenderer;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _particleRenderer = _particleSystem.GetComponent<ParticleSystemRenderer>();
        _container.OnSubstanceChanged += Revisualize;
    }

    private void StateClarification()
    {
        switch (_container.Substance.State)
        {
            case ChemicalSubstance.MatterState.Liquid:
                LiquidSetup();
                break;
            case ChemicalSubstance.MatterState.Solid:
                SolidSetup();
                break;
            default:
                break;
        }
    }

    public void Play()
    {
        _particleSystem.Play();
    }

    private void LiquidSetup()
    {
        ParticleSystem.TrailModule trailModule = _particleSystem.trails;
        trailModule.enabled = true;
    }

    private void SolidSetup()
    {
        ParticleSystem.TrailModule trailModule = _particleSystem.trails;
        trailModule.enabled = false;
    }

    protected override void Revisualize()
    {
        ChemicalSubstance substance = _container.Substance;
        if (substance != null)
        {
            StateClarification();
            _particleRenderer.sharedMaterial = substance.Material;
            _particleRenderer.trailMaterial = substance.Material;
        }
    }
}