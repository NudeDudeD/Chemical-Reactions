using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SaturationValueBox : MonoBehaviour, IPointerClickHandler, IDragHandler
{   
    [SerializeField] private RectTransform _handle;
    private Material _rawImageMaterial;
    private RectTransform _rectTransform;
    private int _width;
    private int _height;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

        RawImage rawImage = GetComponent<RawImage>();
        _rawImageMaterial = rawImage.materialForRendering;

        _width = (int)_rectTransform.sizeDelta.x;
        _height = (int)_rectTransform.sizeDelta.y;

        ColorPicker.HueUpdated += UpdateGraphics;
        ColorPicker.SaturationUpdated += MoveHandleVertically;  
        ColorPicker.ValueUpdated += MoveHandleHorizontally;
    }

    private void MoveHandleVertically(float saturation)
    {
        Vector2 position = _handle.anchoredPosition;
        position.x = saturation * _width;
        _handle.anchoredPosition = position;
    }

    private void MoveHandleHorizontally(float value)
    {
        Vector2 position = _handle.anchoredPosition;
        position.y = value * _height;
        _handle.anchoredPosition = position;
    }

    public void OnHold(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        Vector2 wheelPosition = new Vector2(_rectTransform.position.x, _rectTransform.position.y);
        Vector2 halfSize = new Vector2(_width / 2f, _height / 2f);
        Vector2 position = eventData.position - wheelPosition + halfSize;

        position.x = Mathf.Clamp(position.x, 0f, _width);
        position.y = Mathf.Clamp(position.y, 0f, _height);

        ColorPicker.Saturation = position.x / _width;
        ColorPicker.Value = position.y / _height;
    }

    public void OnDrag(PointerEventData eventData) => OnHold(eventData);
    public void OnPointerClick(PointerEventData eventData) => OnHold(eventData);

    public void UpdateGraphics(float hue) => _rawImageMaterial.SetFloat("_Hue", hue);
}