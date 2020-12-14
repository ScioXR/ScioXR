using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Collections;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public static AppManager instance;

    public string currentSceneName;
    public bool loaded = true;

    private string dataPath;
    public string saveDirectory = "Experiences";

    void Awake()
    {
        if (!instance)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;

            dataPath = Path.Combine(Application.persistentDataPath, saveDirectory);
            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public string GetScenesFolder()
    {
        return dataPath;
    }

    public string GetScenePath()
    {
        return Path.Combine(dataPath, currentSceneName + ".json");
    }
}
