using UnityEngine;

public class PlayerInputHandlerInvoker : MonoBehaviour
{
    public void Enable() => PlayerInputHolder.Enable();

    public void Disable() => PlayerInputHolder.Disable();
}