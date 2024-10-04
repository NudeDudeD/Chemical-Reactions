using System.Collections.Generic;
using UnityEngine;

public class IconStorage : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Camera _renderingCamera;

    private static IconStorage _instance;
    private static List<Pair<Substance, Texture>> _textures = new List<Pair<Substance, Texture>>();

    public static Texture GetTexture(Substance substance) => _textures.Find(p => p.Key.Compare(substance))?.Value;

    public static void Initialize()
    {
        foreach (Pair<Substance, MaterialSettings> pair in DataStorage.SubstancesInformation)
        {
            _instance._renderer.sharedMaterial = pair.Value.Material;
            _instance._renderingCamera.Render();

            RenderTexture.active = _instance._renderingCamera.activeTexture;

            int width = RenderTexture.active.width;
            int height = RenderTexture.active.height;
            Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            texture.Apply();
            RenderTexture.active = null;

            Pair<Substance, Texture> textureData = new Pair<Substance, Texture>(pair.Key, texture);
            _textures.Add(textureData);
        }
    }

    private void Awake()
    {
        _instance = this;
    }
}