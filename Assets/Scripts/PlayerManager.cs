using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (AppManager.instance.currentSceneName != "")
        {
            AppManager.instance.loaded = false;
            //Debug.Log("Loading: " + AppManager.instance.currentSceneName);
            SaveCollection dataJson = ScioXRSceneManager.instance.LoadFromJson(AppManager.instance.GetScenePath());
            ScioXRSceneManager.instance.SetEnvironment(dataJson.environment);
            MessagesManager.instance.InitMessages(dataJson.globalData.messages);
            
            ScioXRSceneManager.instance.CreateLoadedObjects(dataJson, false);
        }
    }
}
