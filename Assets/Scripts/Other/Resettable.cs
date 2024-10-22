using UnityEngine;
using UnityEngine.Events;

public class Resettable : MonoBehaviour
{
    [SerializeField] private UnityEvent _onReset;
    [SerializeField] private Resetter _resetter;

    private void Awake()
    {
        _resetter.OnReset += _onReset.Invoke;
    }
}