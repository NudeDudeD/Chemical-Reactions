using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PairedStorage<T1, T2> where T1 : class, IComparable<T1> where T2 : class
{
    protected readonly string _path;
    protected List<Pair<T1, T2>> _storage = new List<Pair<T1, T2>>();

    public event Action<T1, T2> OnElementAdded = delegate { };
    public event Action<T1, T2> OnElementRemoved = delegate { };

    public Pair<T1, T2> this[int index]
    {
        get => _storage[index];
        set => _storage[index] = value;
    }

    public Pair<T1, T2> Find(T1 key) => _storage.Find(p => p.Key == key);
    public Pair<T1, T2> FindReference(T1 key) => _storage.Find(t => t.Key.CompareTo(key) > 0);
    public bool Exists(T1 key) => Find(key) != null;
    public bool ExistsReference(T1 key) => FindReference(key) != null;
    public int Count => _storage.Count;

    public T2 FindValue(T1 key)
    {
        Pair<T1, T2> pair = Find(key);
        return pair == null ? default : pair.Value;
    }

    public PairedStorage(string filePath)
    {
        _path = Path.Combine(Application.persistentDataPath, filePath);
    }

    public void Load()
    {
        try
        {
            _storage = JsonDataLoader.LoadList<Pair<T1, T2>>(_path);
        }
        catch (Exception)
        {
            _storage = new List<Pair<T1, T2>>();
        }
    }

    public void Save()
    {
        JsonDataLoader.SaveList(_storage, _path);
    }

    public bool Add(T1 key, T2 value)
    {
        if (ExistsReference(key))
            return false;

        Pair<T1, T2> pair = new Pair<T1, T2>(key, value);
        _storage.Add(pair);
        OnElementAdded(pair.Key, pair.Value);
        return true;
    }

    public bool Remove(T1 key)
    {
        Pair<T1, T2> pair = Find(key);
        if (pair == null)
            return false;

        _storage.Remove(pair);
        OnElementRemoved(pair.Key, pair.Value);
        return true;
    }
}