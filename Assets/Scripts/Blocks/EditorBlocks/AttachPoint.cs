using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPoint : MonoBehaviour
{
    protected bool IsEnabled = true;

    public Transform stickPoint;
    public RectTransform hoverRect;
    public GameObject heighlight;

    

    public bool CheckIsInside(GameObject otherObject)
    {
        if (!IsEnabled || otherObject == gameObject)
        {
            return false;
        }

        bool inside = hoverRect.Overlaps(otherObject.GetComponent<RectTransform>());

        //Vector3 diff = hoverRect.transform.position - otherObject.transform.position;
        //bool inside = hoverRect.rect.Contains(diff);
        heighlight.SetActive(inside);
        return inside;
    }

    public void Enable(bool isEnabled)
    {
        IsEnabled = isEnabled;
        if (!IsEnabled)
        {
            heighlight.SetActive(false);
        }
    }
}
