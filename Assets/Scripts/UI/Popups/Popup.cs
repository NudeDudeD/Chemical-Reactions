public abstract class Popup : UITransitioner
{
    public virtual void Initialize()
    {
        SetInstance();
        gameObject.SetActive(false);
    }

    public override void Switch()
    {     
        OnClosing();
        base.Switch();
    }

    protected abstract void OnClosing();
    protected abstract void SetInstance();
}