using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SimpleSelectable), true)]
public class SimpleSelectableEditor : Editor
{
    private SimpleSelectable _target;

    public override void OnInspectorGUI()
    {
        if (_target == null)
            _target = ((SimpleSelectable)target);

        base.OnInspectorGUI();
        _target.Interactable = EditorGUILayout.Toggle("Interactable", _target.Interactable);
    }
}