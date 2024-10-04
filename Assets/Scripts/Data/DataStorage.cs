using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class DataStorage
{
    private const string _substancesInformationFileName = "substancesInformation.json";
    private const string _reactionsFileName = "reactions.json";
    private static string _substancesInformationPath;
    private static string _reactionsPath;

    private static List<Pair<Substance, MaterialSettings>> _substancesInformation;
    private static List<Reaction> _reactions;

    public static List<Pair<Substance, MaterialSettings>> SubstancesInformation => _substancesInformation;
    public static List<Reaction> Reactions => _reactions;

    public static void Initialize()
    {
        _substancesInformationPath = GetPath(_substancesInformationFileName);
        _reactionsPath = GetPath(_reactionsFileName);

        Load();
    }

    public static void Save()
    {
        JsonDataLoader.SaveList(_substancesInformation, _substancesInformationPath);
        JsonDataLoader.SaveList(_reactions, _reactionsPath);
    }

    public static void Load()
    {
        _substancesInformation = JsonDataLoader.LoadList<Pair<Substance, MaterialSettings>>(_substancesInformationPath);
        foreach (Pair<Substance, MaterialSettings> pair in _substancesInformation)
            pair.Value.Initialize();

        _reactions = JsonDataLoader.LoadList<Reaction>(_reactionsPath);
    }

    private static string GetPath(string fileName) => Path.Combine(Application.persistentDataPath, fileName);

    private static Pair<Substance, MaterialSettings> GetPair(Substance substance) => SubstancesInformation.Find(pair => pair.Key.Compare(substance));

    public static Substance GetSubstance(Substance substance)
    {
        Pair<Substance, MaterialSettings> pair = GetPair(substance);
        if (pair != null)
            return pair.Key;
        return null;
    }

    public static Material GetMaterial(Substance substance)
    {
        Pair<Substance, MaterialSettings> pair = GetPair(substance);
        if (pair != null)
            return pair.Value.Material;
        return null;
    }

    public static bool AddSubstance(Substance substance, MaterialSettings materialSettings)
    {
        Pair<Substance, MaterialSettings> pair = new Pair<Substance, MaterialSettings>(substance, materialSettings);
        if (GetPair(substance) != null)
            return false;

        _substancesInformation.Add(pair);
        return true;
    }
}