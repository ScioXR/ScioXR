using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveAsPanel : XRPanel
{
    public InputField inputField;
    public override void Show()
    {
        base.Show();

    }

    public void SaveCurrentScene()
    {
        ScioXRSceneManager.instance.SaveScene(AppManager.instance.GetScenePath());
    }

    public void SaveAsScene()
    {
        AppManager.instance.currentSceneName = inputField.text;
        ScioXRSceneManager.instance.SaveScene(AppManager.instance.GetScenePath());
    }
}
