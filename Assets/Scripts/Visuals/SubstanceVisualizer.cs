using System;
using UnityEngine;

public abstract class SubstanceVisualizer : MonoBehaviour
{
    [SerializeField] protected SubstanceContainer _container;
    protected abstract void Revisualize();
}