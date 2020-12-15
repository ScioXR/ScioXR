using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsPanel : XRPanel
{
    public Text sceneName;
    public Button saveButton;
    public override void Show()
    {
        base.Show();
        sceneName.text = AppManager.instance.currentSceneName != "" ? AppManager.instance.currentSceneName : "<UNTITLED SCENE>";
        saveButton.GetComponent<Button>().interactable = AppManager.instance.currentSceneName != "";
    }

    public void GoToMainMenu()
    {
        AppManager.instance.currentSceneName = "";
        SceneManager.LoadScene("Main");
    }
}
