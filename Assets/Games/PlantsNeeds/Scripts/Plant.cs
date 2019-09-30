using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public GameObject[] leafs;
    public GameObject staff;
    public Material green;
    public Material brown;
    private bool isGreen;
    private bool isWatered;
    public bool inDark;
    public bool inLight;
    private Renderer rend;
    public float scaleSize = 1.5f;
    public float speed = 2.0f;
    private float startTime;

    //tutorial
    public bool tutorialLightShown;
    public bool tutorialDarkShown;
    public bool tutorialPlantDied;

    void Start()
    {
        isGreen = true;
        leafs = GameObject.FindGameObjectsWithTag("Leaf");
        rend = GetComponent<Renderer>();
        startTime = Time.time;
    }


    void Update()
    {
        //tutorial
        if (!isGreen && !tutorialPlantDied)
        {
            tutorialPlantDied = true;
            TutorialManager.instance.PlantUnderSunText();
        }
        if (inDark && !tutorialDarkShown)
        {
            tutorialDarkShown = true;
            TutorialManager.instance.HidePlantText();
        }
        if (inLight && !tutorialLightShown)
        {
            tutorialLightShown = true;
            TutorialManager.instance.UseWateringCan();
        }

    }


    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Dark")
        {
            if(isGreen)
            {
                inDark = true;
                foreach (GameObject gameObject in leafs)
                {
                    if(gameObject.tag == "Leaf")
                    {                      
                        gameObject.GetComponent<Renderer>().material = brown;           //Change color in brown.
                        isGreen = false;
                    }                  
                }                                  
            }
        }
        else if (collision.gameObject.tag == "Light")
        {
           
            if (!isGreen)
            {
                inLight = true;
                foreach (GameObject gameObject in leafs)
                {
                    if (gameObject.tag == "Leaf")
                    {                      
                        gameObject.GetComponent<Renderer>().material = green;         //Change color in green.
                        isGreen = true;
                    }                  
                }             
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Dark")
        {
            inDark = false;
        }
        if (other.gameObject.tag == "Light")
        {
            inLight = false;
        }
    }

    public void ScaleSize()
    {     
        if(isGreen == true && !isWatered)
        {
            inLight = false;
            inDark = false;
            TutorialManager.instance.PlantHasGrown();
            staff.transform.localScale += new Vector3(scaleSize, scaleSize, scaleSize);
            isWatered = true;          
        }
    }


    private void OnParticleCollision(GameObject other)
    {      
        if (other.name == "WaterParticle")
        {
            ScaleSize();
        }
    }

}
