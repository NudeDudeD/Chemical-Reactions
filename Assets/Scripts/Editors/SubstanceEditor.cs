using System;
using UnityEngine;
using UnityEngine.Events;

public class SubstanceEditor : MonoBehaviour
{
    [SerializeField] private ControllableSubstanceContainer substanceContainer;
    [SerializeField] private UnityEvent OnCreated;
    private MaterialSettings _materialSettings;
    private string _substanceName;
    private Substance.MatterState _matterState;

    private void OnEnable()
    {
        ResetParameters();
        IconRenderer.SetActive(true);
    }

    private void OnDisable()
    {
        IconRenderer.SetActive(false);
    }

    private void ResetParameters()
    {
        _materialSettings = new MaterialSettings(Color.black, 0f, 0f);
        IconRenderer.Material = _materialSettings.Material;
    }

    private float ConvertColorComponent(string colorComponent)
    {
        if (colorComponent.Length == 0)
            return 0f;
        return Mathf.Clamp01(float.Parse(colorComponent) / 255f);
    }

    public void Create()
    {
        if (_substanceName == null || _substanceName.Length < 3)
        {
            MessageBox.Show("Error", "The name should be at least 3 characters long.");
            return;
        }

        Substance substance = new Substance(_substanceName, _matterState);
        if (!DataStorage.SubstanceInfo.Add(substance, _materialSettings))
        {
            MessageBox.Show("Error", "Error creating a substance.");
            return;
        }

        ResetParameters();
        OnCreated.Invoke();
        substanceContainer.GetInputRequest(substance);
    }

    public void SetSubstanceName(string substanceName) => _substanceName = substanceName;

    public void SetSubstanceMatterState(int matterState) => _matterState = (Substance.MatterState)matterState;

    public void SetAlbedoRed(string red)
    {
        Color albedo = _materialSettings.Albedo;
        _materialSettings.Albedo = new Color(ConvertColorComponent(red), albedo.g, albedo.b, albedo.a);
    }

    public void SetAlbedoGreen(string green)
    {
        Color albedo = _materialSettings.Albedo;
        _materialSettings.Albedo = new Color(albedo.r, ConvertColorComponent(green), albedo.b, albedo.a);
    }

    public void SetAlbedoBlue(string blue)
    {
        Color albedo = _materialSettings.Albedo;
        _materialSettings.Albedo = new Color(albedo.r, albedo.g, ConvertColorComponent(blue), albedo.a);
    }

    public void SetAlbedoAlpha(string alpha)
    {
        Color albedo = _materialSettings.Albedo;
        float alphaComponent = ConvertColorComponent(alpha);
        _materialSettings.TransparencyEnabled = alphaComponent != 1;
        _materialSettings.Albedo = new Color(albedo.r, albedo.g, albedo.b, alphaComponent);
    }

    public void SetMetallic(float metallic) => _materialSettings.Metallic = metallic;

    public void SetSmoothness(float smoothness) => _materialSettings.Smoothness = smoothness;

    public void SetSpecularHighlightsEnabled(bool isEnabled) => _materialSettings.SpecularHighlightsEnabled = isEnabled;

    public void SetReflectionsEnabled(bool isEnabled) => _materialSettings.ReflectionsEnabled = isEnabled;

    public void SetEmissionEnabled(bool isEnabled) => _materialSettings.EmissionEnabled = isEnabled;

    public void SetEmissionRed(string red)
    {
        Color emission = _materialSettings.Emission;
        _materialSettings.Emission = new Color(ConvertColorComponent(red), emission.g, emission.b);
    }

    public void SetEmissionGreen(string green)
    {
        Color emission = _materialSettings.Emission;
        _materialSettings.Emission = new Color(emission.r, ConvertColorComponent(green), emission.b);
    }

    public void SetEmissionBlue(string blue)
    {
        Color emission = _materialSettings.Emission;
        _materialSettings.Emission = new Color(emission.r, emission.g, ConvertColorComponent(blue));
    }
}