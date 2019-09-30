using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhysicsStats : MonoBehaviour
{
    public Rigidbody objectToTrack;
    public TextMeshPro text;

    public Image potentialBar;
    public Image kineticBar;

    float maxVelocity;
    float lastVelocity;

    public bool running;

    public bool upwards;

    // Start is called before the first frame update
    void Start()
    {
        potentialBar.transform.localScale = new Vector3(potentialBar.transform.localScale.x, 0, potentialBar.transform.localScale.z);
        kineticBar.transform.localScale = new Vector3(kineticBar.transform.localScale.x, 0, kineticBar.transform.localScale.z);
       
    }

    // Update is called once per frame
    void Update()
    {
        
        if (running)
        {

            float kineticEnergy = objectToTrack.velocity.magnitude / 6;
            float potentialEnergy = 0;

            if (maxVelocity > 0)
            {
                potentialEnergy = Mathf.Abs(maxVelocity - kineticEnergy);
                
            }

            //float potentialEnergy = objectToTrack.angularVelocity.magnitude / 2;
            text.text = "V: " + potentialEnergy + ", Max: " + maxVelocity;
            potentialBar.transform.localScale = new Vector3(potentialBar.transform.localScale.x, potentialEnergy, potentialBar.transform.localScale.z);
            kineticBar.transform.localScale = new Vector3(kineticBar.transform.localScale.x, kineticEnergy, kineticBar.transform.localScale.z);

            if (lastVelocity > kineticEnergy && !upwards)
            {
                maxVelocity = lastVelocity;
                upwards = true;
            }

            if (lastVelocity < kineticEnergy && upwards)
            {
                upwards = false;
            }

            lastVelocity = kineticEnergy;
           
        }
    }

    public void StartStats(bool enable)
    {
       // Debug.Log("StartStats");
        
        running = enable;
        if (enable)
        {
            TutorialManagerPendulum.instance.HideEnterText();
        }
        
    }

}
