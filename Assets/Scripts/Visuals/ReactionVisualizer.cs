using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ReactionVisualizer : MonoBehaviour
{   
    [SerializeField] private ReactionHandler _reactionHandler;
    [SerializeField] private Reaction.ReactionEffect _reactionEffect;

    private ParticleSystem _particleSystem;

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