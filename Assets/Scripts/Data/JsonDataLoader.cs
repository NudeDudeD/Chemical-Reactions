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
        json = json[9..^1];
        StreamWriter writer = new StreamWriter(path);
        json = BeautifyJSON(json);
        writer.Write(json);
        writer.Close();
    }

    public static List<T> LoadList<T>(string path)
    {
        if (!File.Exists(path))
            return default;

        StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();
        json = "{\"_list\":" + json + "}";
        reader.Close();

        ListHolder<T> holder = JsonUtility.FromJson<ListHolder<T>>(json);
        return holder.List;
    }

    public static List<T> LoadFromResources<T>(string path)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        string json = textAsset.text;
        json = "{\"_list\":" + json + "}";
        ListHolder<T> holder = JsonUtility.FromJson<ListHolder<T>>(json);
        return holder.List;
    }

    private static string BeautifyJSON(string json)
    {
        string s = string.Empty;
        string tabulation = string.Empty;
        char current = char.MinValue;
        char previous;
        json = json.Replace("\t", string.Empty);
        json = json.Replace("\n", string.Empty);
        for (int i = 0; i < json.Length; i++)
        {
            previous = current; 
            current = json[i];

            if (current == '{' || current == '[')
            {
                if (previous == ':')
                    s += "\n" + tabulation;
                tabulation += "\t";
                s += current + "\n" + tabulation;
            }
            else if (current == '}' || current == ']')
            {
                tabulation = tabulation[0..^1];
                s += "\n" + tabulation + current;
            }
            else if (current == ',')
                s += current + "\n" + tabulation;
            else
                s += current;
        }

        return s;
    }
}
