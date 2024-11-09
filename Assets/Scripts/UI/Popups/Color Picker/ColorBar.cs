using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorBar : MonoBehaviour
{
    [SerializeField] private Graphic _targetGraphic;
    [SerializeField] private UnityEvent<Color> OnColorChanged;
    private Vector2 _topRightCorner;

    private void Awake()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        _topRightCorner = rectTransform.GetCorner(true, true);
    }

    private void ChangeColor(Color color)
    {
        _targetGraphic.color = color;
        OnColorChanged.Invoke(color);
    }

    public void PickColor() => ColorPicker.Show(_topRightCorner, _targetGraphic.color, ChangeColor);

    public void Reset() => _targetGraphic.color = Color.red;
}