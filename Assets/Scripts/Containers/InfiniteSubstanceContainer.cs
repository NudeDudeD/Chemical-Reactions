public class InfiniteSubstanceContainer : SubstanceContainer
{
    public override ChemicalSubstance GetOutputRequest()
    {
        return Substance;
    }
}