using System.Collections.Generic;
using UnityEngine;

public class PanelSorter : MonoBehaviour
{
    [SerializeField] private RectTransform _parent;
    [SerializeField] private Vector2 _startingPoint;
    [SerializeField] private Vector2 _offset;

    private List<RectTransform> _panels = new List<RectTransform>();

    private void SortPanel(int index)
    {
        if (index >= _panels.Count)
            return;

        _panels[index].anchoredPosition = _startingPoint + index * _offset;
        index++;
        if (index < _panels.Count)
            SortPanel(index);
    }

    public void AddPanel(RectTransform panel)
    {
        panel.SetParent(_parent);
        _panels.Add(panel);
        SortPanel(_panels.Count - 1);
    }    

    public void RemovePanel(int index)
    {
        if (index >= _panels.Count)
            return;

        _panels.RemoveAt(index);
        if (index < _panels.Count)
            SortPanel(index);
    }

    public void RemovePanel(RectTransform panel)
    {
        int index = _panels.FindIndex(p => p == panel);
        if (index == -1)
            return;

        RemovePanel(index);
    }
}