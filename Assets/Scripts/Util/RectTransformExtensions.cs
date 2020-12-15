using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RectTransformExtensions
{

    public static bool Overlaps(this RectTransform a, RectTransform b)
    {
        return a.WorldRect().Overlaps(b.WorldRect());
    }
    public static bool Overlaps(this RectTransform a, RectTransform b, bool allowInverse)
    {
        return a.WorldRect().Overlaps(b.WorldRect(), allowInverse);
    }

    public static Rect WorldRect(this RectTransform rectTransform)
    {
        Vector2 sizeDelta = rectTransform.sizeDelta;
        float rectTransformWidth = rectTransform.rect.width * rectTransform.lossyScale.x;
        float rectTransformHeight = rectTransform.rect.height * rectTransform.lossyScale.y;

        Vector3 position = rectTransform.position;
        return new Rect(position.x - rectTransformWidth * rectTransform.pivot.x, position.y - rectTransformHeight * rectTransform.pivot.y, rectTransformWidth, rectTransformHeight);
    }
}
