using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIButtonToggle : MonoBehaviour
{   
    public GameObject on;   
    public GameObject off;

    private bool isOn;

    private void Start()
    {
        Set(isOn);
    }


    public void Toggle()
    {
        Set(!isOn);

        //Debug.Log("ToggleSet " + isOn);
    }

    public void Set(bool enable)
    {
        isOn = enable;
        off.SetActive(!isOn);
        on.SetActive(isOn);
    }

 
}
