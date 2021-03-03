using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LaserGame : MonoBehaviour
{
    public Cage cage;

    public TextMeshPro number;
    
    public float force;

    public GameObject ball;

    public GameObject laser;

    public Vector3 startPosition;

    public GameObject[] buttons;
    public GameObject winMark;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = ball.transform.position;

        winMark.SetActive(false);
    }

    public void ChangeDelay(float value)
    {
        float newValue = float.Parse(number.text) + value;
        if (newValue > 10)
        {
            newValue = 10;
        }
        if (newValue < 0)
        {
            newValue = 0;
        }
        number.text = "" + newValue;
    }

    public void Launch()
    {
        float delay = float.Parse(number.text);
        ball.GetComponent<Rigidbody>().isKinematic = false;
        ball.GetComponent<Rigidbody>().AddForce(new Vector3(0, force, 0));
        Invoke("LaserShow", delay);
    }

    private void LaserShow()
    {
        laser.SetActive(true);
        Invoke("LaserHide", 0.2f);
    }

    private void LaserHide()
    {
        laser.SetActive(false);
    }

    public void Lose()
    {
        ball.GetComponent<Rigidbody>().isKinematic = true;
        ball.transform.position = startPosition;
        CancelInvoke();
        laser.SetActive(false);
    }

    public void Win()
    {
        laser.SetActive(false);
        foreach (var button in buttons)
        {
            Destroy(button.GetComponent<TouchButton>());
        }
        winMark.SetActive(true);

        cage.UnlockLight();
    }
}
