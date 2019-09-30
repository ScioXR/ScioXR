using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManagerMenu : MonoBehaviour
{
    public TextMeshProUGUI game1Title;
    public TextMeshProUGUI game2Title;
    public TextMeshProUGUI game3Title;
    public TextMeshProUGUI game1Description;
    public TextMeshProUGUI game2Description;
    public TextMeshProUGUI game3Description;

    // Start is called before the first frame update
    void Start()
    {
        SetDescriptionText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetDescriptionText()
    {
        game1Title.text = LocalizationManager.instance.GetLocalizedValue("menu_game1_title");
        game1Description.text = LocalizationManager.instance.GetLocalizedValue("menu_game1_description");
        game2Title.text = LocalizationManager.instance.GetLocalizedValue("menu_game2_title");
        game2Description.text = LocalizationManager.instance.GetLocalizedValue("menu_game2_description");
        game3Title.text = LocalizationManager.instance.GetLocalizedValue("menu_game3_title");
        game3Description.text = LocalizationManager.instance.GetLocalizedValue("menu_game3_description");
    }
}
