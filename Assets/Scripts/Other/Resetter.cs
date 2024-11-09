using System;
using UnityEngine;

public class Resetter : MonoBehaviour
{
    public event Action OnReset;

    public void Invoke() => OnReset.Invoke();
}