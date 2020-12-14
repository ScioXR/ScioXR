using Siccity.GLTFUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class AssetsLoader
{
    static string appUrl = Application.absoluteURL;
    //static string appUrl = "http://localhost:8000/";

    static string modelsSufix = ".gltf";

    public static IEnumerator GetModelsList(Action<List<string>> callback)
    {
#if UNITY_WEBGL //&& !UNITY_EDITOR
        yield return GetModelsFromUrl(callback);
#else
        callback(GetFilesInDir(modelsSufix));
        yield return null;
#endif
    }

    public static IEnumerator GetModelsFromUrl(Action<List<string>> callback)
    {
        List<string> modelNames = new List<string>();
        string modelsList = appUrl + "StreamingAssets/models.txt";
        //Debug.Log("Models list: " + modelsList);
        UnityWebRequest www = UnityWebRequest.Get(modelsList);

        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string[] lines = www.downloadHandler.text.Replace("\r", "").Split('\n');
            foreach (var line in lines)
            {
                modelNames.Add(line);
            }
        }
        callback(modelNames);
    }

    protected static List<string> GetFilesInDir(string sufix)
    {
        List<string> modelNames = new List<string>();
        string assetsPath = Application.dataPath + "/StreamingAssets";

        string[] modelEntries = Directory.GetFiles(assetsPath);
        for (int i = 0; i < modelEntries.Length; i++)
        {
            if (!modelEntries[i].Contains("meta") && modelEntries[i].EndsWith(sufix))
            { // Debug.Log("GetModelsList " + fileEntries[i]);
                string fileName = modelEntries[i].Replace('\\', '/');

                int index = fileName.LastIndexOf("/");
                string name = fileName.Remove(0, index + 1);
                if (index >= 0)
                {
                    int stringSufix = name.LastIndexOf(".");
                    string cleanName = name.Remove(stringSufix);
                    modelNames.Add(cleanName);
                    // Debug.Log("file " + clearName);
                }
            }
        }

        return modelNames;
    }

    public static bool CheckIfModelExist(string modelName)
    {
#if UNITY_WEBGL
        //TODO: implement
        return true;
#else
        string modelPath = Path.Combine(Application.dataPath + "/StreamingAssets", modelName);

        string filePath = modelPath + modelsSufix;
        //  Debug.Log("CheckIfModelExist " + filePath);
        if (File.Exists(filePath))
        {
            return true;
        }
        else
        {
            return File.Exists(modelPath);
        }
#endif
    }

    public static IEnumerator ImportModel(string modelName, Action<GameObject> callback)
    {
        GameObject loadedObject = null;
#if UNITY_WEBGL //&& !UNITY_EDITOR
        string modelPath = appUrl + "StreamingAssets/" + modelName + modelsSufix;
        Debug.Log("ImportGLTF: " + modelPath);
        UnityWebRequest www = UnityWebRequest.Get(modelPath);

        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            loadedObject = Importer.LoadFromString(www.downloadHandler.text);
        }
#else
        string modelPath = Application.dataPath + "/StreamingAssets/" + modelName + modelsSufix;
        loadedObject = Importer.LoadFromFile(modelPath);
#endif
        callback(loadedObject);
        yield return null;
    }
}
