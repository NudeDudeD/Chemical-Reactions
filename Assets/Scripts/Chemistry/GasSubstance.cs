using UnityEngine;

[CreateAssetMenu(menuName = "Chemistry/GasSubstance")]
public class GasSubstance : ChemicalSubstance
{
    public GasSubstance(string name, Material material)
    {
        this.name = name;
        _material = material;
    }
}