using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReactionIcon : IconFrame
{
    private const float _deactivatedTransparency = .3f;

    [SerializeField] private Reaction _reaction;
    [SerializeField] private Graphic[] _agentGraphics;
    [SerializeField] private MaskedTextureShifter _effectTextureShifter;
    [SerializeField] private RawImage _reactiveImage;
    [SerializeField] private RawImage _additionalReactiveImage;
    [SerializeField] private RawImage _productImage;
    [SerializeField] private RawImage _additionalProductImage;
    [SerializeField] private TMP_Text _transitionText;

    public Reaction Reaction
    {
        get => _reaction;
        set
        {
            _reaction = value;
            Redraw();
        }
    }

    public override void Redraw()
    {
        _reactiveImage.texture = IconStorage.GetTexture(_reaction.Reactive);
        _reactiveImage.gameObject.SetActive(_reactiveImage.texture != null);
        _additionalReactiveImage.texture = IconStorage.GetTexture(_reaction.AdditionalReactive);
        _additionalReactiveImage.gameObject.SetActive(_additionalReactiveImage.texture != null);
        _productImage.texture = IconStorage.GetTexture(_reaction.Product);
        _productImage.gameObject.SetActive(_productImage.texture != null);
        _additionalProductImage.texture = IconStorage.GetTexture(_reaction.AdditionalProduct);
        _additionalProductImage.gameObject.SetActive(_additionalProductImage.texture != null);

        _effectTextureShifter.ShiftTo((int)_reaction.Effect, 0);
        _name.text = _reaction.Name;
        _transitionText.text = _reaction.WorksInReverse ? "<=>" : "=>";

        for (int i = 0; i < _agentGraphics.Length; i++)
        {
            Color color = _agentGraphics[i].color;
            bool isIn = false;
            for (int j = 0; j < _reaction.Agents.Length; j++)
            {
                if ((int)_reaction.Agents[j] == i)
                {
                    isIn = true;
                    break;
                }
            }
            color.a = isIn ? 1f : _deactivatedTransparency;
            _agentGraphics[i].color = color;
        }
    }
}