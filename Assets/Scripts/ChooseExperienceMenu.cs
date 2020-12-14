using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseExperienceMenu : MonoBehaviour
{
    public GameObject experienceCard;
    public GameObject experienceCardHolder;

    public GameObject browsePanel;
    public GameObject infoPanel;

    public TextMeshProUGUI experienceTitle;

    public string selectedExperience;

    private void Start()
    {
        PopulateExperiences();
    }

    public void PopulateExperiences()
    {
        foreach (Transform child in experienceCardHolder.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        List<string> experienesName = ScioXRSceneManager.instance.GetScenesList(AppManager.instance.GetScenesFolder());
        for (int i = 0; i < experienesName.Count; i++)
        {
            GameObject card = Instantiate(experienceCard, experienceCardHolder.transform);
            card.GetComponentInChildren<Text>().text = experienesName[i];
            string sceneName = experienesName[i];
            card.GetComponentInChildren<Button>().onClick.AddListener(delegate () { ShowExperienceInfo(sceneName); });
        }
    }

    public void NewExperience()
    {
        AppManager.instance.currentSceneName = "";
        SceneManager.LoadScene("Editor");
    }

    public void EditExperience()
    {
        AppManager.instance.currentSceneName = selectedExperience;
        SceneManager.LoadScene("Editor");
    }

    public void PlayExperience()
    {
        AppManager.instance.currentSceneName = selectedExperience;
        SceneManager.LoadScene("Player");
    }

    public void DeleteExperience()
    {
        string filePath = AppManager.instance.GetScenesFolder() + "/" + selectedExperience + ".json";
        File.Delete(filePath);

        PopulateExperiences();
        ShowExperienceList();
    }

    public void ShowExperienceInfo(string experienceName)
    {
        selectedExperience = experienceName;

        browsePanel.SetActive(false);
        infoPanel.SetActive(true);

        experienceTitle.text = selectedExperience;
    }

    public void ShowExperienceList()
    {
        selectedExperience = null;

        browsePanel.SetActive(true);
        infoPanel.SetActive(false);
    }

    
}
