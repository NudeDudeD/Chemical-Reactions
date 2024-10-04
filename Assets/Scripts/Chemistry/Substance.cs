﻿using UnityEngine;

[System.Serializable]
public class Substance
{
    [System.Serializable]
    public enum MatterState
    {
        Liquid,
        Solid,
        Gas
    }

    [SerializeField] private string _name;
    [SerializeField] private MatterState _state;

    public string Name => _name;
    public MatterState State => _state;

    public Substance(string name, MatterState state)
    {
        _name = name;
        _state = state;
    }

    public bool Compare(Substance other) => _name == other._name && _state == other._state;
}