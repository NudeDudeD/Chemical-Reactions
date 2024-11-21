using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Storage<T> where T : class, IComparable<T>
{
    protected readonly string _path;
    protected List<T> _storage = new List<T>();

    public event Action<T> OnElementAdded = delegate { };
    public event Action<T> OnElementRemoved = delegate { };

    public T this[int index]
    {
        get => _storage[index];
        set => _storage[index] = value;
    }

    public T Find(Predicate<T> match) => _storage.Find(match);
    public T Find(T storable) => _storage.Find(t => t == storable);
    public T FindReference(T storable) => _storage.Find(t => t.CompareTo(storable) > 0);
    public bool Exists(T storable) => Find(storable) != null;
    public bool ExistsReference(T storable) => FindReference(storable) != null;
    public int Count => _storage.Count;

    public Storage(string filePath)
    {
        _path = Path.Combine(Application.persistentDataPath, filePath);
    }

    public void Load()
    {
        try
        {
            _storage = JsonDataLoader.LoadList<T>(_path);
        }
        catch (Exception)
        {
            _storage = new List<T>();
        }
    }

    public void Save()
    {
        JsonDataLoader.SaveList(_storage, _path);
    }

    public bool Add(T storable)
    {
        if (ExistsReference(storable))
            return false;

        _storage.Add(storable);
        OnElementAdded(storable);
        return true;
    }

    public bool Remove(T storable)
    {
        if (Find(storable) == null)
            return false;

        _storage.Remove(storable);
        OnElementRemoved(storable);
        return true;
    }
}