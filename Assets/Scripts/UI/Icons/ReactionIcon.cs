using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReactionIcon : IconFrame
{
    private const float _deactivatedTransparency = .3f;

    [SerializeField] private Reaction _reaction;
    [SerializeField] private Graphic[] _agentGraphics;
    [SerializeField] private MaskedTextureShifter _effectTextureShifter;
    [SerializeField] private SubstanceIcon _reactiveIcon;
    [SerializeField] private SubstanceIcon _additionalReactiveIcon;
    [SerializeField] private SubstanceIcon _productIcon;
    [SerializeField] private SubstanceIcon _additionalProductIcon;
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
        if (_reaction == null)
        {
            _reactiveIcon.Substance = _additionalReactiveIcon.Substance = _productIcon.Substance = _additionalProductIcon.Substance = null;
            _effectTextureShifter.ShiftTo(-1, 0);
            _name.text = "No reaction";
            _transitionText.text = string.Empty;

            for (int i = 0; i < _agentGraphics.Length; i++)
            {
                Color color = _agentGraphics[i].color;
                color.a = _deactivatedTransparency;
                _agentGraphics[i].color = color;
            }

            return;
        }

        _reactiveIcon.Substance = _reaction.Reactive;
        _additionalReactiveIcon.Substance = _reaction.AdditionalReactive;
        _productIcon.Substance = _reaction.Product;
        _additionalProductIcon.Substance = _reaction.AdditionalProduct;

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

    public override void Reset() => Reaction = null;
}