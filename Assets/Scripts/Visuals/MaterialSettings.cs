using System;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class MaterialSettings
{
    private Material _material;

    [SerializeField] private Color _albedo;
    [SerializeField] private Color _emission;
    [SerializeField] private float _metallic;
    [SerializeField] private float _smoothness;
    [SerializeField] private bool _specularHighlightsEnabled;
    [SerializeField] private bool _reflectionsEnabled;
    [SerializeField] private bool _emissionEnabled;
    [SerializeField] private bool _transparencyEnabled;

    public Material Material
    {
        get
        {
            if (_material == null)
                Initialize();
            return _material;
        }
    }

    public Color Albedo
    {
        get => _albedo;
        set
        {
            _albedo = value;
            SetAlbedo();
        }
    }

    public Color Emission
    {
        get => _emission;
        set
        {
            _emission = value;
            SetEmission();
        }
    }

    public float Metallic
    {
        get => _metallic;
        set
        {
            _metallic = value;
            SetMetallic();
        }
    }

    public float Smoothness
    {
        get => _smoothness;
        set
        {
            _smoothness = value;
            SetSmoothness();
        }
    }

    public bool SpecularHighlightsEnabled
    {
        get => _specularHighlightsEnabled;
        set
        {
            _specularHighlightsEnabled = value;
            SetSpecularHighlights();
        }
    }

    public bool ReflectionsEnabled
    {
        get => _reflectionsEnabled;
        set
        {
            _reflectionsEnabled = value;
            SetReflections();
        }
    }

    public bool EmissionEnabled
    {
        get => _emissionEnabled;
        set
        {
            _emissionEnabled = value;
            SetEmission();
        }
    }

    public bool TransparencyEnabled
    {
        get => _transparencyEnabled;
        set
        {
            _transparencyEnabled = value;
            SetTransparency();
        }
    }

    public MaterialSettings(Color albedo, float metallic, float smoothness, bool emissionEnabled = false, Color emission = default, bool transparencyEnabled = false, bool specularHighlightsEnabled = true, bool reflectionsEnabled = true)
    {
        _albedo = albedo;
        _emission = emission;
        _metallic = metallic;
        _smoothness = smoothness;
        _specularHighlightsEnabled = specularHighlightsEnabled;
        _reflectionsEnabled = reflectionsEnabled;
        _emissionEnabled = emissionEnabled;
        _transparencyEnabled = transparencyEnabled;

        Initialize();
    }

    public void Initialize()
    {
        if (_material == null)
            _material = new Material(Shader.Find("Standard"));

        SetReflections();
        SetSpecularHighlights();
        SetTransparency();
        SetEmission();
        SetAlbedo();
        SetMetallic();
        SetSmoothness();
    }

    private void SetAlbedo()
    {
        _material.SetColor("_Color", _albedo);
    }

    private void SetMetallic()
    {
        _material.SetFloat("_Metallic", _metallic);
    }

    private void SetSmoothness()
    {
        _material.SetFloat("_Glossiness", _smoothness);
    }

    private void SetEmission()
    {
        if (_emissionEnabled)
        {
            _material.EnableKeyword("_EMISSION");
            _material.SetColor("_EmissionColor", _emission);
        }
        else
            _material.DisableKeyword("_EMISSION");
    }

    private void SetTransparency()
    {
        if (_transparencyEnabled)
        {
            _material.SetFloat("_Mode", 3f);
            _material.SetFloat("_ZWrite", 0f);
            _material.SetInt("_SrcBlend", (int)BlendMode.SrcAlpha);
            _material.SetInt("_DstBlend", (int)BlendMode.OneMinusSrcAlpha);
            _material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            _material.renderQueue = 3000;
        }
        else
        {
            _material.SetFloat("_Mode", 0f);
            _material.SetFloat("_ZWrite", 1f);
            _material.SetInt("_SrcBlend", (int)BlendMode.One);
            _material.SetInt("_DstBlend", (int)BlendMode.Zero);
            _material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            _material.renderQueue = -1;
        }
    }

    private void SetSpecularHighlights()
    {
        if (_specularHighlightsEnabled)
        {
            _material.SetFloat("_SpecularHighlights", 1f);
            _material.DisableKeyword("_SPECULARHIGHLIGHTS_OFF");
        }
        else
        {
            _material.SetFloat("_SpecularHighlights", 0f);
            _material.EnableKeyword("_SPECULARHIGHLIGHTS_OFF");
        }
    }

    private void SetReflections()
    {
        if (_reflectionsEnabled)
        {
            _material.SetFloat("_GlossyReflections", 1f);
            _material.DisableKeyword("_GLOSSYREFLECTIONS_OFF");
        }
        else
        {
            _material.SetFloat("_GlossyReflections", 0f);
            _material.EnableKeyword("_GLOSSYREFLECTIONS_OFF");
        }
    }
}