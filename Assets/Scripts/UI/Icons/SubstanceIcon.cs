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
        if (_substance == null)
        {
            _name.text = "No substance";
            _rawImage.texture = new Texture2D(0, 0);
            _textureShifter.ShiftTo(-1, 0);
        }
        else
        {
            _name.text = _substance.Name;
            _rawImage.texture = IconStorage.GetTexture(_substance);
            _textureShifter.ShiftTo((int)_substance.State, 0);
        }
    }

    public override void Reset() => Substance = null;
}