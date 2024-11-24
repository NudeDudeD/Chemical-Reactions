using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class IconList : MonoBehaviour
{
    [SerializeField] private RectTransform _parent;
    [SerializeField] private Vector2 _startingPoint;
    [SerializeField] private Vector2 _offset;
    [SerializeField] protected IconFrame _iconPrefab;
    [SerializeField] protected bool _onlyOneSelect;

    protected List<IconFrame> _icons = new List<IconFrame>();
    protected List<IconFrame> _selectedIcons = new List<IconFrame>();

    protected abstract string NoSelectedMessage { get; }
    protected abstract string DeletionMessage { get; }

    private void SortIcon(int index)
    {
        if (index >= _icons.Count)
            return;

        RectTransform rectTransform = _icons[index].GetComponent<RectTransform>();
        Vector2 position = _startingPoint + index * _offset;
        rectTransform.anchoredPosition = position;
        
        index++;
        if (index < _icons.Count)
            SortIcon(index);
    }

    private void ResizeParent() => _parent.sizeDelta = new Vector2(_startingPoint.x * 2 + (_icons.Count - 1) * _offset.x, _parent.sizeDelta.y);

    private void AddSelectedIcon(SelectableGraphic sender)
    {
        if (_onlyOneSelect)
            for (int i = _selectedIcons.Count - 1; i >= 0; i--)
                _selectedIcons[i].Selected = false;
        IconFrame icon = (IconFrame)sender;
        _selectedIcons.Add(icon);
    }

    private void RemoveSelectedIcon(SelectableGraphic sender) => _selectedIcons.Remove((IconFrame)sender);

    protected void AddIcon(IconFrame icon)
    {
        icon.OnSelect += AddSelectedIcon;
        icon.OnDeselect += RemoveSelectedIcon;

        RectTransform rectTransform = icon.GetComponent<RectTransform>();
        rectTransform.SetParent(_parent);
        _icons.Add(icon);
        ResizeParent();
        SortIcon(_icons.Count - 1);
    }

    protected void RemoveIcon(int index)
    {
        if (index >= _icons.Count || index < 0)
            return;

        _icons[index].OnSelect -= AddSelectedIcon;
        _icons[index].OnDeselect -= RemoveSelectedIcon;
        Destroy(_icons[index].gameObject); //pool of reusable icons is a good idea, have no time to implement it though
        _icons.RemoveAt(index);
        ResizeParent();
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

    protected abstract void RemoveSelected();

    public void RequestRemoveSelected()
    {
        if (_selectedIcons.Count == 0)
        {
            MessageBox.Show("Information", NoSelectedMessage);
            return;
        }

        string message = DeletionMessage;
        foreach (IconFrame icon in _selectedIcons)
            message += '\n' + icon.Text;
        MessageBox.Show("Deletion", message, (callback) => { if (callback) RemoveSelected(); }, MessageBox.Buttons.OkCancel);
    }

    public void DeselectAll()
    {
        for (int i = _selectedIcons.Count - 1; i >= 0; i--)
            _selectedIcons[i].Selected = false;
    }

    public void Filter(string name)
    {
        if (name == string.Empty)
        {
            foreach (var icon in _icons)
                if (icon.Interactable == false)
                    icon.Interactable = true;
            return;
        }

        name = name.ToLowerInvariant();
        foreach (var icon in _icons)
        {
            bool alike = false;
            string iconName = new string(icon.Text.Where(c => char.IsLetter(c) || c == ' ').ToArray());
            iconName = iconName.ToLowerInvariant();
            string[] strings = iconName.Split(' ');
            foreach (string s in strings)
                if (s.StartsWith(name))
                {
                    alike = true;
                    break;
                }

            if (!alike)
                icon.Interactable = false;
            else if (!icon.Interactable)
                icon.Interactable = true;
        }
    }
}