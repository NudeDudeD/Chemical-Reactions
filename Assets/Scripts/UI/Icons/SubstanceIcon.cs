using UnityEngine;

public class SubstanceIcon : IconFrame
{
    [SerializeField] private Substance _substance;

    public Substance Substance
    {
        get => _substance;
        set
        {
            _substance = value;
            Redraw(_substance.Name, IconStorage.GetTexture(_substance));
        }
    }
}