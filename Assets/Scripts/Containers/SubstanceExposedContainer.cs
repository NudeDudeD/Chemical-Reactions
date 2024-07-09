using System;
using UnityEngine;

public class SubstanceExposedContainer : SubstanceContainer
{
    private static float _angleToSpill = 55f;
    public event Action OnSubstanceSpilled = delegate { };

    private void FixedUpdate()
    {
        if (Substance != null && Vector3.Angle(transform.up, Vector3.up) > _angleToSpill)
        {
            Substance = null;
            OnSubstanceSpilled.Invoke();
        }
    }
}