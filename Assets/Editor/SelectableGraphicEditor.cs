using UnityEditor;

[CustomEditor(typeof(SelectableGraphic), true)]
public class SelectableGraphicEditor : Editor
{
    private SelectableGraphic _target;

    public override void OnInspectorGUI()
    {
        if (_target == null)
            _target = (SelectableGraphic)target;

        base.OnInspectorGUI();
        _target.Interactable = EditorGUILayout.Toggle("Interactable", _target.Interactable);
    }
}