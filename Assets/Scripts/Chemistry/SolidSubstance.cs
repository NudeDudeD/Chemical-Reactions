using UnityEngine;

[CreateAssetMenu(menuName = "Chemistry/SolidSubstance")]
public class SolidSubstance : ChemicalSubstance
{
    public SolidSubstance(string name, Material material)
    {
        this.name = name;
        _material = material;
    }
}