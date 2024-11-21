public interface IInteractable
{
    public string Name { get; }
    public void Interact(Interactor interactor = null);
}