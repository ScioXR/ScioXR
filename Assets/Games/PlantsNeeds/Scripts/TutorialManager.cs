using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public GameObject plantPanelLight;
    public TextMeshProUGUI grabPlantLightText;
    public TextMeshProUGUI sunText;
    public TextMeshProUGUI darkText;

    public GameObject plantPanelDark;
    public TextMeshProUGUI grabPlantDarkText;

    public GameObject plant;
    public static TutorialManager instance;


    // Start is called before the first frame update
    void Start()
    {
        sunText.text = LocalizationManager.instance.GetLocalizedValue("plantneeds_ingame_ text");
        darkText.text = LocalizationManager.instance.GetLocalizedValue("plantneeds_ingame_ text1");
        MovePlantInDark();
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void MovePlantInDark()
    {
       // Debug.Log("Tutorial 2");
        plantPanelLight.SetActive(true);
        grabPlantLightText.text = LocalizationManager.instance.GetLocalizedValue("plantneeds_step1_info");
      
    }

    public void HidePlantText()
    {
        grabPlantLightText.text = "";
    }

    public void PlantUnderSunText()
    {
        //Debug.Log("Tutorial 3");
        plantPanelLight.SetActive(false);
        plantPanelDark.SetActive(true);
        grabPlantDarkText.text = LocalizationManager.instance.GetLocalizedValue("plantneeds_step2_info");
    }

    public void HideUnderSunText()
    {
        plantPanelLight.SetActive(false);
        grabPlantLightText.text = "";
    }

    public void UseWateringCan()
    {
        //Debug.Log("Tutorial 4");
        plantPanelLight.SetActive(true);
        plantPanelDark.SetActive(false);
        grabPlantLightText.text = LocalizationManager.instance.GetLocalizedValue("plantneeds_step3_info");
    }

    public void PlantHasGrown()
    {
        //Debug.Log("Tutorial 5");
        grabPlantLightText.text = LocalizationManager.instance.GetLocalizedValue("plantneeds_step4_info");
       // Invoke("HidePlantHasGrown", 3f);
    }

    public void HidePlantHasGrown()
    {
        plantPanelLight.SetActive(false);
    }
}
