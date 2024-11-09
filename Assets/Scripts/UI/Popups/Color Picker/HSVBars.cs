using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class HSVBars : MonoBehaviour
{
    [SerializeField] private Scrollbar _hueScrollbar; 
    [SerializeField] private Scrollbar _saturationScrollbar; 
    [SerializeField] private Scrollbar _valueScrollbar;
    private Material _saturationMaterial;
    private Material _valueMaterial;

    private void Awake()
    {
        RawImage saturationImage = _saturationScrollbar.GetComponent<RawImage>();
        _saturationMaterial = saturationImage.materialForRendering;
        RawImage valueImage = _valueScrollbar.GetComponent<RawImage>();
        _valueMaterial = valueImage.materialForRendering;

        ColorPicker.HueUpdated += MoveHue;
        ColorPicker.HueUpdated += UpdateHue;
        ColorPicker.SaturationUpdated += MoveSaturaton;
        ColorPicker.SaturationUpdated += UpdateSaturation;
        ColorPicker.ValueUpdated += MoveValue;
        ColorPicker.ValueUpdated += UpdateValue;
    }

    private void UpdateHue(float hue)
    {
        _saturationMaterial.SetFloat("_Hue", hue);
        _valueMaterial.SetFloat("_Hue", hue);
    }

    private void UpdateValue(float value) => _saturationMaterial.SetFloat("_Value", value);

    private void UpdateSaturation(float saturation) => _valueMaterial.SetFloat("_Saturation", saturation);

    private void MoveHue(float hue) => _hueScrollbar.value = hue;

    private void MoveSaturaton(float saturation) => _saturationScrollbar.value = saturation;

    private void MoveValue(float value) => _valueScrollbar.value = value;
}