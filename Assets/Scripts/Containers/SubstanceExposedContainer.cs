using UnityEngine;

public class SubstanceExposedContainer : SubstanceContainer
{
    [SerializeField] SubstanceParticleVisualizer _spillVisualizer;
    private static float _angleToSpill = 55f;

    private void FixedUpdate()
    {
        if (Substance != null && Vector3.Angle(transform.up, Vector3.up) > _angleToSpill)
        {
            Substance = null;
            _spillVisualizer.Play();
        }
    }
}