public abstract class Popup : UITransitioner
{
    public virtual void Initialize()
    {
        SetInstance();
        _onSwitchedAway.AddListener(OnClosing);
        gameObject.SetActive(false);
    }

    protected abstract void OnClosing();
    protected abstract void SetInstance();
}