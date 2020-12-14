using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMenu : XRPanel
{
    public TextMeshProUGUI sceneNameText;

    public override void Show()
    {
        base.Show();

        sceneNameText.text = AppManager.instance.currentSceneName;
    }

    public void GoToMainMenu()
    {
        AppManager.instance.currentSceneName = "";
        SceneManager.LoadScene("Main");
    }

    public void EditExperience()
    {
        //AppManager.instance.currentSceneName = selectedExperience;
        SceneManager.LoadScene("Editor");
    }

    public void PlayExperience()
    {
        //AppManager.instance.currentSceneName = selectedExperience;
        SceneManager.LoadScene("Player");
    }
}
