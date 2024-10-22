using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class JsonDataLoader
{
    [System.Serializable]
    private struct ListHolder<T>
    {
        [SerializeField] private List<T> _list;

        public List<T> List => _list;

        public ListHolder(List<T> list)
        {
            _list = list;
        }
    }

    public static void SaveList<T>(List<T> list, string path)
    {
        ListHolder<T> holder = new ListHolder<T>(list);
        string json = JsonUtility.ToJson(holder);
        StreamWriter writer = new StreamWriter(path);
        writer.Write(json);
        writer.Close();
    }

    public static List<T> LoadList<T>(string path)
    {
        if (!File.Exists(path))
            return default;

        StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();
        reader.Close();

        ListHolder<T> holder;

        holder = JsonUtility.FromJson<ListHolder<T>>(json);
        return holder.List;
    }
}
