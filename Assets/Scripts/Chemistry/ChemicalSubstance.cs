using UnityEngine;

[CreateAssetMenu(menuName = "Chemistry/Chemical Substance")]
public class ChemicalSubstance : ScriptableObject
{
    public enum MatterState
    {
        Liquid,
        Solid
    }

    [SerializeField] protected Material _material;
    [SerializeField] protected MatterState _state;

    public Material Material => _material;
    public MatterState State => _state;
}