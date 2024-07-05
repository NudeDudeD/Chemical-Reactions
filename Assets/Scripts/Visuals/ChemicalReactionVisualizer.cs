using UnityEngine;

public class ChemicalReactionVisualizer : MonoBehaviour
{
    [SerializeField] private ChemicalReactionHandler _reactionHandler;
    private ParticleSystem _particleSystem;
    [SerializeField] private ChemicalReaction.ReactionEffect _reactionEffect;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _reactionHandler.OnReactionPerformed += Revisualize;
    }

    private void Revisualize()
    {
        if (_reactionHandler.LatestReaction.Effect == _reactionEffect)
            _particleSystem.Play();
    }
}