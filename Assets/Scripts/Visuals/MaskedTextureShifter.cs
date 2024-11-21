using UnityEngine;

public class MaskedTextureShifter : MonoBehaviour
{
    [SerializeField] private RectTransform _target;
    [SerializeField] private Vector2 _shift;

    public void ShiftTo(int indexX, int indexY) => _target.anchoredPosition = new Vector2(_shift.x * indexX, _shift.y * indexY);
}