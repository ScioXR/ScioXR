using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    public ParticleSystem waterParticle;
    public GameObject endPoint;
    public bool shouldPlay;
    public bool isDownward;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Vector3.Angle(this.transform.up, Vector3.up);
        isDownward = angle > 20.0f;
        // Debug.Log("isDownward " + angle);
        if (isDownward)
        {
            shouldPlay = true;
        } else
        {
            shouldPlay = false;
        }
        Watering();
    }

    private void PressSpace()
    {

    }

    public void Watering()
    {
        if (shouldPlay)
        {
            shouldPlay = false;
            waterParticle.Play();
           // Debug.Log("particle play");
        } else
        {
            waterParticle.Stop();
        }

    }
}
