using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour
{

    public static LocalizationManager instance;

    private Dictionary<string, string> localizedText;
    private bool isReady = false;
    private string missingTextString = "Localized text not found";

    public enum Language
    {
        EN,
        RS
    }

    public Language language;

    public TextAsset csv;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        LoadLocalizedText();

        //VerboseLocalization();

        //Debug.Log("AC: " + Resources.Load<AudioClip>("en/win"));
    }

    public void LoadLocalizedText()
    {
        string[,] loadedData = CSVReader.SplitCsvGrid(csv.text);
        localizedText = new Dictionary<string, string>();
        for (int i = 1; i < loadedData.GetLength(1); i++)
        {
            if (loadedData[0, i] != null)
            {
                //Debug.Log(loadedData[0, i] + " ### " + loadedData[(int)language + 1, i]);
                localizedText.Add(loadedData[0, i], loadedData[(int)language + 1, i]);
            }
        }

        
        //string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        //if (File.Exists(filePath))
        //{
        //    string dataAsJson = File.ReadAllText(filePath);
        //    LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

        //    for (int i = 0; i < loadedData.items.Length; i++)
        //    {
        //        localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
        //    }

        //    Debug.Log("Data loaded, dictionary contains: " + localizedText.Count + " entries");
        //}
        //else
        //{
        //    Debug.LogError("Cannot find file!");
        //}

        isReady = true;
    }

    public void VerboseLocalization()
    {
        foreach (string key in localizedText.Keys)
        {
            Debug.Log("" + key + ", " + localizedText[key]);
        }
    }

    public string GetLocalizedValue(string key)
    {
        string result = missingTextString;
        if (localizedText.ContainsKey(key))
        {
            result = localizedText[key];
        }

        return result;

    }

    public bool GetIsReady()
    {
        return isReady;
    }

    public AudioClip GetLocalizedAudio(string key)
    {
        //AssetLoader.LoadAsset<T>(key, CheckLanguageOverrideCode(localizedObject));
        AudioClip audioClip = Resources.Load<AudioClip>(language.ToString() + "/" + key);
        return audioClip;
    }

    public void ChangeLanguageEN()
    {
        language = Language.EN;
        LoadLocalizedText();
    }
    public void ChangeLanguageRS()
    {
        language = Language.RS;
        LoadLocalizedText();
    }
}