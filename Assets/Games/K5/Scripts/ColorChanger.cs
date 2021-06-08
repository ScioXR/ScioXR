using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private Color baseColor;

    public Color touchColor;
    public float touchColorDelay;
    public bool shouldReset;

    private void Start()
    {
        baseColor = GetComponent<MeshRenderer>().material.color;
    }

    public void ChangeColor()
    {
        GetComponent<MeshRenderer>().material.color = touchColor;
        CancelInvoke();
        if (shouldReset)
        {
            Invoke("ResetColor", touchColorDelay);
        }
    }

    public void ResetColor()
    {
        GetComponent<MeshRenderer>().material.color = baseColor;
    }
}
