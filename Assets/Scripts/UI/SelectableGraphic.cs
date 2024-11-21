using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Graphic))]
public class SelectableGraphic : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private enum State
    {
        Normal,
        Highlighted,
        Pressed,
        Disabled
    }

    [SerializeField] private Graphic _targetGraphic;
    [SerializeField] private ColorBlock _colors;
    
    private State _state;
    private bool _selected;
    private bool _pointerDown;
    public event Action<SelectableGraphic> OnSelect = delegate { };
    public event Action<SelectableGraphic> OnDeselect = delegate { };

    public bool Interactable
    {
        get => _state != State.Disabled;
        set
        {
            Selected = false;
            _state = value ? State.Normal : State.Disabled;
            UpdateGraphics(true);
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

    private void UpdateGraphics(bool instant = false)
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
            UpdateGraphics();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _pointerDown = false;
        if (_state != State.Disabled)
        {
            _state = State.Normal;
            UpdateGraphics();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left || _state == State.Disabled)
            return;

        _state = State.Pressed;
        UpdateGraphics();
    }

    public void OnPointerUp(PointerEventData eventData) //this damn method ALSO triggers whenever the cursor is MOVED after press, why Unity?
    {
        if (eventData.button != PointerEventData.InputButton.Left || eventData.dragging || !_pointerDown || _state == State.Disabled )
            return;

        Selected = !Selected;
        _state = State.Highlighted;
        UpdateGraphics();
    }
}