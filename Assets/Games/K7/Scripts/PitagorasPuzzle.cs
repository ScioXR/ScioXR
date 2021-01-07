using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PitagorasPuzzle : MonoBehaviour
{
    public int solution;
    public TextMeshPro solutionNumber;
    public GameObject submitButton;

    public Cage cage;

    public void Unlock()
    {
        if (solution == int.Parse(solutionNumber.text))
        {
            submitButton.GetComponent<MeshRenderer>().material.color = Color.green;
            Destroy(submitButton.GetComponent<TouchButton>());
            cage.UnlockLight();
        }
        else
        {
            submitButton.GetComponent<MeshRenderer>().material.color = Color.red;
            CancelInvoke();
            Invoke("ResetButton", 0.5f);
        }
    }

    public void ChangeNumber(int increment)
    {
        int newValue = int.Parse(solutionNumber.text) + increment;
        if (newValue >= 99)
        {
            newValue = 0;
        }
        if (newValue < 0)
        {
            newValue = 99;
        }
        solutionNumber.text = "" + newValue;
    }

    public void ResetButton()
    {
        submitButton.GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
