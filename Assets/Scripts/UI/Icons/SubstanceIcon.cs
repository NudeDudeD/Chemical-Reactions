using UnityEngine;
using UnityEngine.UI;

public class SubstanceIcon : IconFrame
{
    [SerializeField] private MaskedTextureShifter _textureShifter;
    [SerializeField] private Substance _substance;
    [SerializeField] private RawImage _rawImage;

    public Substance Substance
    {
        get => _substance;
        set
        {
            _substance = value;
            Redraw();
        }
    }

    public override void Redraw()
    {
        _name.text = _substance.Name;
        _rawImage.texture = IconStorage.GetTexture(_substance);
        _textureShifter.ShiftTo((int)_substance.State, 0);
    }
}