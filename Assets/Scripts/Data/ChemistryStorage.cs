using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ChemistryStorage
{
    public class Storage<T> where T : IComparable<T>
    {
        private readonly string _path;
        private List<T> _storage = new List<T>();

        public event Action<T> OnElementAdded;
        public event Action<T> OnElementRemoved;

        public T Get(T storable) => _storage.Find(t => t.CompareTo(storable) > 0);
        public List<T> List => _storage;

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
            if (Get(storable) != null)
                return false;

            _storage.Add(storable);
            OnElementAdded(storable);
            return true;
        }

        public bool Remove(T storable)
        {
            if (Get(storable) != null)
                return false;

            _storage.Remove(storable);
            OnElementRemoved(storable);
            return true;
        }
    }

    public class PairedStorage<T1, T2> where T1 : IComparable<T1>
    {
        private readonly string _path;
        private List<Pair<T1, T2>> _storage = new List<Pair<T1, T2>>();

        public event Action<T1, T2> OnElementAdded;
        public event Action<T1, T2> OnElementRemoved;

        public List<Pair<T1, T2>> List => _storage;
        public Pair<T1, T2> Find(T1 key) => _storage.Find(p => p.Key.CompareTo(key) > 0);
        public bool Exists(T1 key) => Find(key) != null;
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
            if (Find(key) != null)
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

    private static PairedStorage<Substance, MaterialSettings> _substanceInfo;
    private static Storage<Reaction> _reactions;

    public static PairedStorage<Substance, MaterialSettings> SubstanceInfo => _substanceInfo;
    public static Storage<Reaction> Reactions => _reactions;

    public static void Initialize()
    {
        _substanceInfo = new PairedStorage<Substance, MaterialSettings>("substancesInformation.json");
        _reactions = new Storage<Reaction>("reactions.json");

        _substanceInfo.Load();
        _reactions.Load();
    }

    public static void Save()
    {
        _substanceInfo?.Save();
        _reactions?.Save();
    }
}