public static class ChemistryStorage
{
    private static PairedStorage<Substance, MaterialSettings> _substanceInfo;
    private static Storage<Reaction> _reactions;

    public static PairedStorage<Substance, MaterialSettings> SubstanceInfo => _substanceInfo;
    public static Storage<Reaction> Reactions => _reactions;

    public static void Initialize()
    {
        _substanceInfo = new PairedStorage<Substance, MaterialSettings>("substancesInformation.json");
        _reactions = new Storage<Reaction>("reactions.json");

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