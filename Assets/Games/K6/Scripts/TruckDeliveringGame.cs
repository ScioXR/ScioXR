using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TruckDeliveringGame : MonoBehaviour
{
    public int solution;
    public GameObject[] showItems;

    public TextMeshPro[] numbers;

    public GameObject submitButton;

    public XRCanvas winPanel;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in showItems)
        {
            item.SetActive(false);
        }

        //TESTING
        StartGame();
    }

    public void StartGame()
    {
        foreach (var item in showItems)
        {
            item.SetActive(true);
        }
    }

    public void SubmitResult()
    {
        int result = 0;
        for (int i = 0; i < numbers.Length; i++)
        {
            result += int.Parse(numbers[i].text) * (int)Mathf.Pow(10, i);
        }
        if (result == solution)
        {
            submitButton.GetComponent<MeshRenderer>().material.color = Color.green;
            Invoke("WinGame", 2);
        } else
        {
            submitButton.GetComponent<MeshRenderer>().material.color = Color.red;
            Invoke("ButtonColorReset", 1);
        }
    }

    private void ButtonColorReset()
    {
        submitButton.GetComponent<MeshRenderer>().material.color = Color.white;
    }

    private void WinGame()
    {
        winPanel.Toggle();
    }
}
