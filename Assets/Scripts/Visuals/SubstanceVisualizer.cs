using UnityEngine;

public abstract class SubstanceVisualizer : MonoBehaviour
{
    [SerializeField] protected SubstanceContainer _container;

    protected abstract void Revisualize();

    protected virtual void Start()
    {
        Revisualize();
    }
}