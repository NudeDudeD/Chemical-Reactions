using UnityEngine;

[RequireComponent(typeof(ParticleSystem), typeof(ParticleSystemRenderer))]
public class SubstanceParticleVisualizer : SubstanceVisualizer
{
    private ParticleSystem _particleSystem;
    private ParticleSystemRenderer _particleRenderer;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _particleRenderer = _particleSystem.GetComponent<ParticleSystemRenderer>();
        _container.OnSubstanceChanged += Revisualize;
        ((SubstanceExposedContainer)_container).OnSubstanceLeaked += Play;
    }

    private void StateClarification()
    {
        switch (_container.Substance.State)
        {
            case Substance.MatterState.Liquid:
                LiquidSetup();
                break;
            case Substance.MatterState.Solid:
                SolidSetup();
                break;
            default:
                break;
        }
    }

    private void Play()
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
        Substance substance = _container.Substance;
        if (substance != null)
        {
            StateClarification();
            _particleRenderer.material = DataStorage.SubstanceInfo.FindValue(substance).Material; 
            _particleRenderer.trailMaterial = DataStorage.SubstanceInfo.FindValue(substance).Material;
        }
    }
}