using System.Collections.Generic;
using UnityEngine;

public abstract class IconList : MonoBehaviour
{
    [SerializeField] private RectTransform _parent;
    [SerializeField] private Vector2 _startingPoint;
    [SerializeField] private Vector2 _offset;

    private List<IconFrame> _icons = new List<IconFrame>();
    protected List<IconFrame> _selectedIcons = new List<IconFrame>();

    private void SortIcon(int index)
    {
        if (index >= _icons.Count)
            return;

        RectTransform rectTransform = _icons[index].GetComponent<RectTransform>();
        Vector2 position = _startingPoint + index * _offset;
        rectTransform.anchoredPosition = position;
        _parent.sizeDelta = new Vector2(position.x + _startingPoint.x, _parent.sizeDelta.y);
        index++;
        if (index < _icons.Count)
            SortIcon(index);
    }

    private void AddSelectedIcon(SimpleSelectable sender) => _selectedIcons.Add((IconFrame)sender);

    private void RemoveSelectedIcon(SimpleSelectable sender) => _selectedIcons.Remove((IconFrame)sender);

    protected void AddIcon(IconFrame icon)
    {
        icon.OnSelect += AddSelectedIcon;
        icon.OnDeselect += RemoveSelectedIcon;

        RectTransform rectTransform = icon.GetComponent<RectTransform>();
        rectTransform.SetParent(_parent);
        _icons.Add(icon);
        SortIcon(_icons.Count - 1);
    }

    protected void RemoveIcon(int index)
    {
        if (index >= _icons.Count || index < 0)
            return;

        _icons[index].OnSelect -= AddSelectedIcon;
        _icons[index].OnDeselect -= RemoveSelectedIcon;

        _icons.RemoveAt(index);
        if (index < _icons.Count)
            SortIcon(index);
    }

    protected void RemoveIcon(IconFrame icon)
    {
        int index = _icons.FindIndex(i => i == icon);
        if (index == -1)
            return;

        RemoveIcon(index);
    }

    protected void RemoveIcon(string text)
    {
        int index = _icons.FindIndex(i => i.Text == text);
        if (index == -1)
            return;

        RemoveIcon(index);
    }

    public void DeselectAll()
    {
        for (int i = _selectedIcons.Count - 1; i >= 0; i--)
            _selectedIcons[i].Selected = false;
    }
}