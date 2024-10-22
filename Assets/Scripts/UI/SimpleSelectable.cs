using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Graphic))]
public class SimpleSelectable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private enum State
    {
        Normal,
        Highlighted,
        Pressed,
        Disabled
    }

    public delegate void SelectableAction(SimpleSelectable sender);

    [SerializeField] private Graphic _targetGraphic;
    [SerializeField] private ColorBlock _colors;
    
    private State _state;
    private bool _selected;
    private bool _pointerDown;
    public event SelectableAction OnSelect;
    public event SelectableAction OnDeselect;

    public bool Interactable
    {
        get => _state != State.Disabled;
        set
        {
            _state = value ? State.Normal : State.Disabled;
            UpdateGraphics(true);
            _selected = false;
        }
    }

    public bool Selected
    {
        get => _selected;
        set
        {
            if (_selected == value)
                return;

            _selected = value;
            if (_state == State.Normal)
                UpdateGraphics(true);
            if (_selected)
                OnSelect.Invoke(this);
            else
                OnDeselect.Invoke(this);
        }
    }

    protected virtual void OnEnable()
    {
        if (_state == State.Highlighted || _state == State.Pressed)
            _state = State.Normal;
        UpdateGraphics(true);
    }

    private void UpdateGraphics(bool instant)
    {
        switch (_state)
        {
            case State.Normal:
                FadeTo((_selected ? _colors.selectedColor : _colors.normalColor) * _colors.colorMultiplier, instant);
                break;
            case State.Highlighted:
                FadeTo(_colors.highlightedColor * _colors.colorMultiplier, instant);
                break;
            case State.Pressed:
                FadeTo(_colors.pressedColor * _colors.colorMultiplier, instant);
                break;
            case State.Disabled:
                FadeTo(_colors.disabledColor * _colors.colorMultiplier, instant);
                break;
        }
    }

    private void FadeTo(Color targetColor, bool instant)
    {
        if (_targetGraphic == null)
            return;        
        _targetGraphic.CrossFadeColor(targetColor, instant ? 0f : _colors.fadeDuration, true, true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _pointerDown = true;
        if (_state != State.Disabled)
        { 
            _state = State.Highlighted;
            UpdateGraphics(false);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _pointerDown = false;
        if (_state != State.Disabled)
        {
            _state = State.Normal;
            UpdateGraphics(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left || _state == State.Disabled)
            return;
        _state = State.Pressed;
        UpdateGraphics(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left || !_pointerDown || _state == State.Disabled)
            return;
        Selected = !Selected;
        _state = State.Highlighted;
        UpdateGraphics(false);
    }
}