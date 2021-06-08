using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CheckNumber : MonoBehaviour
{
    public TextMeshPro numberText;
    public int correctNumber;

    public UnityEvent onCorrect;
    public UnityEvent onWrong;

    public void Check()
    {
        if (int.Parse(numberText.text) == correctNumber)
        {
            onCorrect.Invoke();
        } else
        {
            onWrong.Invoke();
        }
    }
}
