using System;
using UnityEngine;

public class Resetter : MonoBehaviour
{
    public event Action OnReset = delegate { };

    public void Invoke() => OnReset.Invoke();
}