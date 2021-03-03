using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunflower : MonoBehaviour
{
    public int targetAngle;
    public GameObject rotatingPoint;

    public GameObject winMark;

    public GameObject[] buttons;

    public Cage cage;
    // Start is called before the first frame update
    void Start()
    {
        winMark.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Rotate(int angle)
    {
        rotatingPoint.transform.Rotate(0, 0, angle);
        //Debug.Log("Angle: " + rotatingPoint.transform.eulerAngles.y);
        CheckFinish();
    }

    public void CheckFinish()
    {
        if (targetAngle == rotatingPoint.transform.eulerAngles.y)
        {
            foreach (var button in buttons)
            {
                Destroy(button.GetComponent<TouchButton>());
            }
            winMark.SetActive(true);
            cage.UnlockLight();
        }
    }
}
