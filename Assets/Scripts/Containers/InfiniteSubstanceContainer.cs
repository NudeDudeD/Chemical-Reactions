public class InfiniteSubstanceContainer : SubstanceContainer, IInteractable
{
    public string Name => "Reset";

    public override Substance GetOutputRequest()
    {
        return Substance;
    }

    public void Interact(Interactor interactor = null) => Substance = null;
}