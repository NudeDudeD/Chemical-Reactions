using UnityEngine;

public abstract class ChemicalSubstance : ScriptableObject
{
    [SerializeField] protected Material _material;
    public Material Material => _material;
}