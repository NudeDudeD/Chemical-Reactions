using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ChemistryStorage
{
    private const string _substancesInfoPath = "substancesInformation.json";
    private const string _reactionsPath = "reactions.json";
    private const string _sampleDataPath = "Sample Data";
    private static PairedStorage<Substance, MaterialSettings> _substanceInfo;
    private static Storage<Reaction> _reactions;

    public static PairedStorage<Substance, MaterialSettings> SubstanceInfo => _substanceInfo;
    public static Storage<Reaction> Reactions => _reactions;

    public static void Initialize()
    {          
        string substancesPath = Path.Combine(Application.persistentDataPath, _substancesInfoPath);
        string reactionsPath = Path.Combine(Application.persistentDataPath, _reactionsPath);

        if (!File.Exists(substancesPath))
        {
            string samplePath = _sampleDataPath + "/" + _substancesInfoPath[0..^5];
            List<Pair<Substance, MaterialSettings>> substanceList = JsonDataLoader.LoadFromResources<Pair<Substance, MaterialSettings>>(samplePath);
            JsonDataLoader.SaveList(substanceList, substancesPath);
        }

        if (!File.Exists(reactionsPath))
        {
            string samplePath = _sampleDataPath + "/" + _reactionsPath[0..^5];
            List<Reaction> reactionList = JsonDataLoader.LoadFromResources<Reaction>(samplePath);
            JsonDataLoader.SaveList(reactionList, reactionsPath);
        }

        _substanceInfo = new PairedStorage<Substance, MaterialSettings>(_substancesInfoPath);
        _reactions = new Storage<Reaction>(_reactionsPath);

        _substanceInfo.Load();
        _reactions.Load();

        for (int i = 0; i < _reactions.Count; i++)
        {
            Substance reactive = _substanceInfo.FindReference(_reactions[i].Reactive).Key;
            Substance additionalReactive = _substanceInfo.FindReference(_reactions[i].AdditionalReactive)?.Key;
            Substance product = _substanceInfo.FindReference(_reactions[i].Product)?.Key;
            Substance additionalProduct = _substanceInfo.FindReference(_reactions[i].AdditionalProduct)?.Key;

            Reaction reaction = new Reaction(reactive, additionalReactive, product, additionalProduct, _reactions[i].Agents, _reactions[i].Effect, _reactions[i].WorksInReverse);
            _substanceInfo.OnElementRemoved += (substance, _) => { if (reaction.HasSubstance(substance)) _reactions.Remove(reaction); };
            _reactions[i] = reaction;
        }
    }

    public static void Save()
    {
        _substanceInfo?.Save();
        _reactions?.Save();
    }
}