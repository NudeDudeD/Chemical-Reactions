using System;
using UnityEngine;

public class ExposedSubstanceContainer : SubstanceContainer
{
    private const float _angleToLeak = 55f;
    public event Action OnSubstanceLeaked = delegate { };

    protected override void Awake()
    {
        base.Awake();
        OnSubstanceChanged += (substance) => { if (substance?.State == Substance.MatterState.Gas) Leak(); };
    }

    private void FixedUpdate()
    {
        if (Substance != null && Vector3.Angle(transform.up, Vector3.up) > _angleToLeak)
            Leak();
    }

    private void Leak()
    {
        OnSubstanceLeaked.Invoke();
        Substance = null;
    }
}