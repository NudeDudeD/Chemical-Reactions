using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class IconFrame : SelectableGraphic
{
    [SerializeField] protected TMP_Text _name;
    
    public string Text => _name.text;

    public abstract void Redraw();

    public abstract void Reset();
}