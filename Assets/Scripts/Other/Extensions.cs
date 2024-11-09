using UnityEngine;
using UnityEngine.UIElements;

public static class Extensions
{
    public static float CounterclockwiseAngle(this Vector2 from, Vector2 to)
    {
        float angle = Vector2.SignedAngle(from, to);
        if (angle < 0)
            angle = 360 + angle;

        return angle;
    }

    public static Bounds Bounds(this RectTransform rectTransform) => new Bounds(rectTransform.position, rectTransform.sizeDelta);

    public static Vector2 GetCorner(this Bounds bounds, bool right, bool top) => new Vector2
    (
        right ? bounds.max.x : bounds.min.x,
        top ? bounds.max.y : bounds.min.y
    );

    public static Vector2 GetCorner(this RectTransform rectTransform, bool right, bool top) => GetCorner(rectTransform.Bounds(), right, top);

    public static void ArrangeToPlane(this RectTransform rectTransform, Vector2 planeCenter, Vector2 planeSize)
    {
        Vector2 transformHalfSize = rectTransform.sizeDelta / 2f;
        Vector2 planeHalfSize = planeSize / 2f;
        Vector2 min = new Vector2(planeCenter.x - planeHalfSize.x + transformHalfSize.x, planeCenter.y - planeHalfSize.y + transformHalfSize.y);
        Vector2 max = new Vector2(planeCenter.x + planeHalfSize.x - transformHalfSize.x, planeCenter.y + planeHalfSize.y - transformHalfSize.y);
        rectTransform.localPosition = new Vector2
        (
            Mathf.Clamp(rectTransform.localPosition.x, min.x, max.x),
            Mathf.Clamp(rectTransform.localPosition.y, min.y, max.y)
        );
    }
}