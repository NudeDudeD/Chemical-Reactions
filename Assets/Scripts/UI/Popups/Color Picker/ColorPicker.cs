using System;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : Popup
{
    public delegate void ColorDelegate(Color color);

    private static ColorPicker _instance;
    private static ColorDelegate _methodToCall;
    private static RectTransform _rectTransform;
    private static Vector2 _globalHalfSize;
    private static float _hue;
    private static float _saturation = 1f;
    private static float _value = 1f;

    [SerializeField] private Graphic _targetGraphic;

    public static event Action<float> HueUpdated;
    public static event Action<float> SaturationUpdated;
    public static event Action<float> ValueUpdated;

    public static float Hue
    {
        get => _hue;
        set
        {
            if (_hue == value)
                return;

            _hue = value;
            Color color = GetColor();
            _methodToCall?.Invoke(color);
            _instance.UpdateGraphics(color);
            HueUpdated.Invoke(value);
        }
    }

    public static float Saturation
    {
        get => _saturation;
        set
        {
            if (_saturation == value)
                return;

            _saturation = value;
            Color color = GetColor();
            _methodToCall?.Invoke(color);
            _instance.UpdateGraphics(color);
            SaturationUpdated.Invoke(value);
        }
    }

    public static float Value
    {
        get => _value; 
        set
        {
            if (_value == value)
                return;

            _value = value;
            Color color = GetColor();
            _methodToCall?.Invoke(color);
            _instance.UpdateGraphics(color);
            ValueUpdated.Invoke(value);
        }
    }

    private static Color GetColor() => Color.HSVToRGB(_hue, _saturation, _value);

    public static void Show(Vector2 position, Color graphicColor, ColorDelegate methodToCall)
    {
        if (_methodToCall == methodToCall)
            return;

        if (_activeTransitioner == _instance)
            _instance.OnClosing();
        else
        {
            _instance._switchTo = _activeTransitioner;
            _instance.SetAsActiveTransitioner();
            _instance.gameObject.SetActive(true);
        }

        Color.RGBToHSV(graphicColor, out float hue, out float saturation, out float value);
        _hue = hue;
        _saturation = saturation;
        _value = value;
        HueUpdated.Invoke(_hue);
        SaturationUpdated.Invoke(_saturation);
        ValueUpdated.Invoke(_value);
        _instance.UpdateGraphics(graphicColor);            

        position.x += _globalHalfSize.x;
        position.y -= _globalHalfSize.y;
        _rectTransform.position = position;
        _rectTransform.ArrangeToPlane(Vector2.zero, new Vector2(1920f, 1080f));
        _methodToCall = methodToCall;
    }

    private void UpdateGraphics(Color color)
    {
        if (_targetGraphic == null)
            return;
        _targetGraphic.color = color;
    }

    public override void Initialize()
    {
        base.Initialize();
        _rectTransform = _instance.GetComponent<RectTransform>();
        _globalHalfSize = new Vector2(_rectTransform.sizeDelta.x, _rectTransform.sizeDelta.y) / 2f;
    }

    protected override void SetInstance() => _instance = this;

    protected override void OnClosing()
    {
        _methodToCall = delegate { };
    }
}