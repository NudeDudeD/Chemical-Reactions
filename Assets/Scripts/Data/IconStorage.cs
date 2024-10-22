using System.Collections.Generic;
using UnityEngine;

public static class IconStorage
{
    private static List<Pair<Substance, Texture>> _substanceTextures = new List<Pair<Substance, Texture>>();

    public static Texture GetTexture(Substance substance) => _substanceTextures.Find(p => p.Key.CompareTo(substance) > 0)?.Value;

    public static void Initialize()
    {
        DataStorage.SubstanceInfo.OnElementAdded += CreateIcon;

        foreach (Pair<Substance, MaterialSettings> pair in DataStorage.SubstanceInfo.List)
            CreateIcon(pair.Key, pair.Value);
    }

    private static void CreateIcon(Substance substance, MaterialSettings materialSettings)
    {
        Texture2D texture = IconRenderer.RenderCameraImage(materialSettings.Material);

        Pair<Substance, Texture> textureData = new Pair<Substance, Texture>(substance, texture);
        _substanceTextures.Add(textureData);
    }
}