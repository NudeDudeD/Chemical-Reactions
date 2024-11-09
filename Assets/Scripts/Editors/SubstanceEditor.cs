using UnityEngine;
using UnityEngine.Events;

public class SubstanceEditor : MonoBehaviour
{
    [SerializeField] private ControllableSubstanceContainer substanceContainer;
    [SerializeField] private UnityEvent OnCreated;
    private MaterialSettings _materialSettings;
    private string _substanceName;
    private Substance.MatterState _matterState;

    private void OnEnable() => ResetParameters();

    private void ResetParameters()
    {
        _materialSettings = new MaterialSettings(Color.red, 0f, 0f);
        IconRenderer.Material = _materialSettings.Material;
    }

    public void Create()
    {
        if (_substanceName == null || _substanceName.Length < 3)
        {
            MessageBox.Show("Error", "The name should be at least 3 characters long.");
            return;
        }

        Substance substance = new Substance(_substanceName, _matterState);
        if (!ChemistryStorage.SubstanceInfo.Add(substance, _materialSettings))
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

    public void SetAlbedo(Color albedo) => _materialSettings.Albedo = albedo;

    public void SetAlpha(float alpha)
    {
        Color albedo = _materialSettings.Albedo;
        albedo.a = alpha;
        _materialSettings.Albedo = albedo;
        _materialSettings.TransparencyEnabled = alpha != 1;
    }

    public void SetMetallic(float metallic) => _materialSettings.Metallic = metallic;

    public void SetSmoothness(float smoothness) => _materialSettings.Smoothness = smoothness;

    public void SetSpecularHighlightsEnabled(bool isEnabled) => _materialSettings.SpecularHighlightsEnabled = isEnabled;

    public void SetReflectionsEnabled(bool isEnabled) => _materialSettings.ReflectionsEnabled = isEnabled;

    public void SetEmissionEnabled(bool isEnabled) => _materialSettings.EmissionEnabled = isEnabled;

    public void SetEmission(Color emission) => _materialSettings.Emission = emission;
}