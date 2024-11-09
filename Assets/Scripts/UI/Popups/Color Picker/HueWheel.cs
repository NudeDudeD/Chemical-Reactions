using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class HueWheel : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    [SerializeField] private RectTransform _handle;
    [SerializeField] private float _minimalRadius;
    [SerializeField] private float _maximalRadius;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        ColorPicker.HueUpdated += RotateHandle;
    }

    public void RotateHandle(float hue) => _handle.localRotation = Quaternion.Euler(0f, 0f, hue * 360f);

    public void OnHold(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        Vector2 wheelPosition = new Vector2(_rectTransform.position.x, _rectTransform.position.y);
        Vector2 direction = eventData.position - wheelPosition;
        if (!eventData.dragging && (direction.magnitude < _minimalRadius || direction.magnitude > _maximalRadius))
            return;

        float angle = Vector2.right.CounterclockwiseAngle(direction);
        ColorPicker.Hue = angle / 360f;
    }

    public void OnDrag(PointerEventData eventData) => OnHold(eventData);
    public void OnPointerClick(PointerEventData eventData) => OnHold(eventData);
}