using UnityEngine;

public abstract class SubstanceVisualizer : MonoBehaviour
{
    [SerializeField] private SubstanceContainer _container;
    protected SubstanceContainer Container
    { 
        get => _container;
        set
        {
            if (_container != null)
                _container.OnSubstanceChanged -= (_) => Revisualize();

            _container = value;
            if (_container != null)
            {
                _container.OnSubstanceChanged += (_) => Revisualize();
                Revisualize();
            }
        }
    }

    protected abstract void Revisualize();

    protected virtual void Awake()
    {
        if (Container != null)
            Container.OnSubstanceChanged += (_) => Revisualize();
    }

    protected virtual void Start()
    {
        if (Container != null)
            Revisualize();
    }
}