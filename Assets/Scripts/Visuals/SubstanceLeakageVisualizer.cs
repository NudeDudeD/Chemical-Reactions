using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem), typeof(ParticleSystemRenderer))]
public class SubstanceLeakageVisualizer : SubstanceVisualizer
{
    private ParticleSystem _particleSystem;
    private ParticleSystemRenderer _particleRenderer;

    private void StateClarification()
    {
        switch (Container.Substance.State)
        {
            case Substance.MatterState.Liquid:
                ParticleSystemSetup();
                break;
            case Substance.MatterState.Solid:
                ParticleSystemSetup(false, 2f);
                break;  
            case Substance.MatterState.Gas:
                ParticleSystemSetup(false, .5f, -.25f, 2f);
                break;
            default:
                break;
        }
    }

    private void ParticleSystemSetup(bool trailEnabled = true, float startSpeed = 3f, float gravityModifier = 1.5f, float startLifetime = 3f)
    {
        ParticleSystem.TrailModule trailModule = _particleSystem.trails;
        trailModule.enabled = trailEnabled;
        ParticleSystem.MainModule mainModule = _particleSystem.main;
        mainModule.startSpeed = startSpeed;
        mainModule.gravityModifier = gravityModifier;
        mainModule.startLifetime = startLifetime;
    }

    protected override void Awake()
    {
        base.Awake();
        _particleSystem = GetComponent<ParticleSystem>();
        _particleRenderer = _particleSystem.GetComponent<ParticleSystemRenderer>();
        ((ExposedSubstanceContainer)Container).OnSubstanceLeaked += () => { Revisualize(); _particleSystem.Play(); };
    }

    protected override void Revisualize()
    {
        Substance substance = Container.Substance;
        if (substance != null)
        {
            StateClarification();
            _particleRenderer.material = ChemistryStorage.SubstanceInfo.FindValue(substance).Material; 
            _particleRenderer.trailMaterial = ChemistryStorage.SubstanceInfo.FindValue(substance).Material;
        }
    }
}