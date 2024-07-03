using System;
using UnityEditor.Compilation;
using UnityEngine;

public class SubstanceContainer : MonoBehaviour
{
    [SerializeField] protected GameObject _liquidObject;
    [SerializeField] protected LiquidSubstance _containedLiquid;

    public LiquidSubstance ContaintedLiquid => _containedLiquid;
}