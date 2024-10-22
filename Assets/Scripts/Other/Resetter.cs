using System;
using UnityEngine;

public class Resetter : MonoBehaviour
{
    public delegate void Reset();
    public event Reset OnReset;

    public void Invoke() => OnReset.Invoke();
}