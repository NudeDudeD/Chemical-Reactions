using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ChemistryStorage
{
    public class Storage<T> where T : class, IComparable<T>
    {
        private readonly string _path;
        private List<T> _storage = new List<T>();

        public event Action<T> OnElementAdded = delegate { };
        public event Action<T> OnElementRemoved = delegate { };

        public T Find(Predicate<T> match) => _storage.Find(match);
        public T Find(T storable) => _storage.Find(t => t == storable);
        public T FindReference(T storable) => _storage.Find(t => t.CompareTo(storable) > 0);
        public bool Exists(T storable) => Find(storable) != null;
        public bool ExistsReference(T storable) => FindReference(storable) != null;
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
            if (ExistsReference(storable))
                return false;

            _storage.Add(storable);
            OnElementAdded(storable);
            return true;
        }

        public bool Remove(T storable)
        {
            if (Find(storable) != null)
                return false;

            _storage.Remove(storable);
            OnElementRemoved(storable);
            return true;
        }
    }

    public class PairedStorage<T1, T2> where T1 : class, IComparable<T1> where T2 : class
    {
        private readonly string _path;
        private List<Pair<T1, T2>> _storage = new List<Pair<T1, T2>>();

        public event Action<T1, T2> OnElementAdded = delegate { };
        public event Action<T1, T2> OnElementRemoved = delegate { };

        public List<Pair<T1, T2>> List => _storage;
        public Pair<T1, T2> Find(T1 key) => _storage.Find(p => p.Key == key);
        public Pair<T1, T2> FindReference(T1 key) => _storage.Find(t => t.Key.CompareTo(key) > 0);
        public bool Exists(T1 key) => Find(key) != null;  
        public bool ExistsReference(T1 key) => FindReference(key) != null;

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

        for (int i = 0; i < _reactions.List.Count; i++)
        {
            Substance reactive = _substanceInfo.FindReference(_reactions.List[i].Reactive)?.Key;
            Substance additionalReactive = _substanceInfo.FindReference(_reactions.List[i].AdditionalReactive)?.Key;
            Substance product = _substanceInfo.FindReference(_reactions.List[i].Product)?.Key;
            Substance additionalProduct = _substanceInfo.FindReference(_reactions.List[i].AdditionalProduct)?.Key;
            _reactions.List[i] = new Reaction(reactive, additionalReactive, product, additionalProduct, _reactions.List[i].Agents, _reactions.List[i].Effect, _reactions.List[i].WorksInReverse);
        }

        //Substance water = _substanceInfo.List.Find(s => s.Key.Name == "Water").Key;
        //Substance copper = _substanceInfo.List.Find(s => s.Key.Name == "Copper").Key;
        //Substance magnesium = _substanceInfo.List.Find(s => s.Key.Name == "Magnesium").Key;
        //Substance magnesiumHydroxide = _substanceInfo.List.Find(s => s.Key.Name == "Magnesium Hydroxide").Key;
        //Substance copperHydroxide = _substanceInfo.List.Find(s => s.Key.Name == "Copper Hydroxide").Key;
        //Substance magnesiumSulfate = _substanceInfo.List.Find(s => s.Key.Name == "Magnesium Sulfate").Key;
        //Substance copperSulfate = _substanceInfo.List.Find(s => s.Key.Name == "Copper Sulfate").Key;

        //Reaction[] reactions = new Reaction[6]
        //{
        //    new Reaction(water, magnesium, magnesiumHydroxide, null, new Reaction.Agent[0], Reaction.VisualEffect.Default),
        //    new Reaction(water, copper, copperHydroxide, null, new Reaction.Agent[0], Reaction.VisualEffect.Default),
        //    new Reaction(magnesiumHydroxide, copper, copperHydroxide, magnesium, new Reaction.Agent[0], Reaction.VisualEffect.Default, true),
        //    new Reaction(magnesiumSulfate, copper, copperSulfate, magnesium, new Reaction.Agent[0], Reaction.VisualEffect.Default, true),
        //    new Reaction(copperSulfate, magnesiumHydroxide, magnesiumSulfate, copperHydroxide, new Reaction.Agent[0], Reaction.VisualEffect.Default, true),
        //    new Reaction(magnesium, null, null, null, new Reaction.Agent[1] { Reaction.Agent.Heat }, Reaction.VisualEffect.Explosion)
        //};

        //for (int i = 0; i < reactions.Length; i++)
        //{
        //    Debug.Log(reactions[i].Name);
        //    Debug.Log(_reactions.List[i]);
        //    Debug.Log(reactions[i].CompareTo(_reactions.List[i]));
        //}

        //Debug.Log(r1.Name);
        //Debug.Log(r2.Name);
        //Debug.Log(r3.Name);
        //Debug.Log(r4.Name);
        //Debug.Log(r5.Name);
        //Debug.Log(r6.Name);

        //_reactions.Add(r1);
        //_reactions.Add(r2);
        //_reactions.Add(r3);
        //_reactions.Add(r4);
        //_reactions.Add(r5);
        //_reactions.Add(r6);
    }

    public static void Save()
    {
        _substanceInfo?.Save();
        _reactions?.Save();
    }
}