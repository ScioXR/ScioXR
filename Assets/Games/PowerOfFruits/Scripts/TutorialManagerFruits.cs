using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class TutorialManagerFruits : MonoBehaviour
{
    public TextMeshProUGUI fruitsText;
    public GameObject panelFruitsText;
   
    // Start is called before the first frame update
    void Start()
    {
        PowerFruits();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PowerFruits()
    {
        // Debug.Log("Tutorial1");
        panelFruitsText.SetActive(true);
        fruitsText.text = LocalizationManager.instance.GetLocalizedValue("fruits_step1_text1");
    }
}
