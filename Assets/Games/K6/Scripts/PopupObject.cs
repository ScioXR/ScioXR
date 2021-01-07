using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupObject : MonoBehaviour
{
    public void Show(float time)
    {
        gameObject.SetActive(true);
        Invoke("Hide", time);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
