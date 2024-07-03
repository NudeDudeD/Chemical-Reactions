using UnityEngine;

public class Flask : SubstanceContainer
{
    private static float _angleToSpill = 55f;
    [SerializeField] private ParticleSystem _spillParticles;

    private bool _isSpilled = false;

    private void FixedUpdate()
    {
        if (!_isSpilled && Vector3.Angle(transform.up, Vector3.up) > _angleToSpill)
        {
            _liquidObject.SetActive(false);
            _spillParticles.Play();
            _isSpilled = true;
        }
    }
}