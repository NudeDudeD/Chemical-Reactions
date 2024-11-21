using System.Collections.Generic;
using UnityEngine;

public static class IconStorage
{
    private static List<Pair<Substance, Texture>> _substanceTextures;

    public static Texture GetTexture(Substance substance) => _substanceTextures.Find(p => p.Key == substance)?.Value;

    public static void Initialize()
    {
        _substanceTextures = new List<Pair<Substance, Texture>>();

        ChemistryStorage.SubstanceInfo.OnElementAdded += CreateIcon;

        for (int i = 0; i < ChemistryStorage.SubstanceInfo.Count; i++)
        {
            Pair<Substance, MaterialSettings> pair = ChemistryStorage.SubstanceInfo[i];
            CreateIcon(pair.Key, pair.Value);
        }
    }

    private static void CreateIcon(Substance substance, MaterialSettings materialSettings)
    {
        Texture2D texture = IconRenderer.RenderCameraImage(materialSettings.Material);

        Pair<Substance, Texture> textureData = new Pair<Substance, Texture>(substance, texture);
        _substanceTextures.Add(textureData);
    }
}