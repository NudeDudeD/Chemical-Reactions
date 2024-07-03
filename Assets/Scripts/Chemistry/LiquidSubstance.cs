using UnityEngine;

[CreateAssetMenu(menuName = "Chemistry/LiquidSubstance")]
public class LiquidSubstance : ChemicalSubstance
{
    public LiquidSubstance(string name, Material material)
    {
        this.name = name;
        _material = material;
    }
}