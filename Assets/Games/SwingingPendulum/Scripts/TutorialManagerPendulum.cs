using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class TutorialManagerPendulum : MonoBehaviour
{
    public TextMeshProUGUI pendulumText;
    public TextMeshPro potentialEnergyText;
    public TextMeshPro kineticEnergyText;
    public TextMeshProUGUI Text;
    public GameObject pendulumPanelText;
    public static TutorialManagerPendulum instance;

    // Start is called before the first frame update
    void Start()
    {
        potentialEnergyText.text = LocalizationManager.instance.GetLocalizedValue("pendulum_ingame_text");
        kineticEnergyText.text = LocalizationManager.instance.GetLocalizedValue("pendulum_ingame_text1");

        GrabAndSwingText();
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

   

    public void HideEnterText()
    {
        //pendulumPanelText.SetActive(false);
        //Debug.Log("HideText");
        ShowBarGraphText();
        
    }

    public void GrabAndSwingText()
    {
        //Debug.Log("Tutorial2");
        pendulumPanelText.SetActive(true);
        pendulumText.text = LocalizationManager.instance.GetLocalizedValue("pendulum_step1_info");
    }

    public void ShowBarGraphText()
    {
       //Debug.Log("Tutorial3");
       // pendulumPanelText.SetActive(true);
        pendulumText.text = LocalizationManager.instance.GetLocalizedValue("pendulum_step2_info");


    }
}
