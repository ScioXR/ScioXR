using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArrowButton : MonoBehaviour
{
    public int increment;
    public int minValue = 0;
    public int maxValue = 10;
    

    public TextMeshPro numberText;

    public void Change()
    {
        int newValue = int.Parse(numberText.text) + increment;
        if (newValue >= maxValue)
        {
            newValue = minValue;
        }
        if (newValue <= minValue)
        {            
            newValue = maxValue + increment;
        }
        numberText.text = "" + newValue;
    }
}
