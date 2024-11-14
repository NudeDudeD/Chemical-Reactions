using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ReactionVisualizer : MonoBehaviour
{   
    [SerializeField] private ReactionHandler _reactionHandler;
    [SerializeField] private Reaction.VisualEffect _reactionEffect;

    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _reactionHandler.OnReactionPerformed += Revisualize;
    }

    private void Revisualize(Reaction reaction)
    {
        if (reaction.Effect == _reactionEffect)
            _particleSystem.Play();
    }
}