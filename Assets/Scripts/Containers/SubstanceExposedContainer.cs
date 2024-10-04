using System;
using UnityEngine;

public class SubstanceExposedContainer : SubstanceContainer
{
    private static float _angleToLeak = 55f;
    public event Action OnSubstanceLeaked = delegate { };

    private void FixedUpdate()
    {
        if (Substance != null && Vector3.Angle(transform.up, Vector3.up) > _angleToLeak)
        {
            Substance = null;
            OnSubstanceLeaked.Invoke();
        }
    }
}