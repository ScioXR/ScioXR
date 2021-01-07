using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArrowButton : MonoBehaviour
{
    public int increment;

    public TextMeshPro numberText;

    public void Change()
    {
        int newValue = int.Parse(numberText.text) + increment;
        if (newValue >= 10)
        {
            newValue = 0;
        }
        if (newValue < 0)
        {
            newValue = 9;
        }
        numberText.text = "" + newValue;
    }
}
