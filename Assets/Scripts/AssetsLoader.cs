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

    static string modelsSuffix = ".gltf";
    static string modelsFolder = "/Models/";
    static string environmentFolder = "/Environments/";

    static string texturesSuffix = ".png";
    static string texturesFolder = "/Textures/";

    public static IEnumerator GetEnvironmentList(Action<List<string>> callback)
    {
#if UNITY_WEBGL //&& !UNITY_EDITOR
        yield return GetModelsFromUrl(Application.streamingAssetsPath + environmentFolder + "files.txt", callback);
#elif UNITY_ANDROID
        yield return GetModelsFromUrl(Application.streamingAssetsPath + environmentFolder + "files.txt", callback);
#else
        callback(GetFilesInDir(environmentFolder, modelsSuffix));
        yield return null;
#endif
    }

    public static IEnumerator GetTexturesList(Action<List<string>> callback)
    {
#if UNITY_WEBGL //&& !UNITY_EDITOR
        yield return GetModelsFromUrl(Application.streamingAssetsPath + texturesFolder + "files.txt", callback);
#elif UNITY_ANDROID
        yield return GetModelsFromUrl(Application.streamingAssetsPath + texturesFolder + "files.txt", callback);
#else
        callback(GetFilesInDir(texturesFolder, texturesSuffix));
        yield return null;
#endif
    }

    public static IEnumerator GetModelsList(Action<List<string>> callback)
    {
#if UNITY_WEBGL //&& !UNITY_EDITOR
        yield return GetModelsFromUrl(Application.streamingAssetsPath + modelsFolder + "files.txt", callback);
#elif UNITY_ANDROID
        yield return GetModelsFromUrl(Application.streamingAssetsPath + modelsFolder + "files.txt", callback);
#else
        callback(GetFilesInDir(modelsFolder, modelsSuffix));
        yield return null;
#endif
    }

    public static IEnumerator GetBasicModelsList(Action<List<string>> callback)
    {
        Debug.Log("GetBasicModelsList");
        callback(GetFilesInResources());
        yield return null;
    }

    public static List<string> GetBasicModelsList()
    {
        Debug.Log("GetBasicModelsList List");
        List<string> modelNames = new List<string>();
        TextAsset mytxtData = (TextAsset)Resources.Load("files");
        string txt = mytxtData.text;
 
        string[] lines = txt.Replace("\r", "").Split('\n');
        foreach (var line in lines)
        {
            modelNames.Add(line);
        }
        return modelNames;
    }

    public static IEnumerator GetModelsFromUrl(string filesList, Action<List<string>> callback)
    {
        List<string> modelNames = new List<string>();
        UnityWebRequest www = UnityWebRequest.Get(filesList);

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
    protected static List<string> GetFilesInResources()
    {
        List<string> modelNames = new List<string>();
        // string assetsPath = Application.dataPath + "/StreamingAssets" + folder;

        List<string> modelEntries = new List<string>();
        UnityEngine.Object[] models = Resources.LoadAll("Models", typeof(GameObject));
        foreach (var name in models)
        {
            modelEntries.Add(name.ToString());           
        }   
        for (int i = 0; i < modelEntries.Count; i++)
        {  
          // Debug.Log("GetModelsList " + modelEntries[i]);
            string fileName = modelEntries[i];  
            int index = fileName.IndexOf(" ");
            string stringSufix = fileName.Remove(0, index);
            string name = fileName.Replace(stringSufix, ".");
            int sufix = name.LastIndexOf(".");
            string cleanName = name.Remove(sufix);
           // Debug.Log("cleanName " + cleanName);
            modelNames.Add(cleanName);
        }

        return modelNames;
    }

    protected static List<string> GetFilesInDir(string folder, string sufix)
    {
        List<string> modelNames = new List<string>();
        string assetsPath = Application.dataPath + "/StreamingAssets" + folder;

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
#if UNITY_WEBGL || UNITY_ANDROID
        //TODO: implement
        return true;
#else
        string modelPath = Path.Combine(Application.dataPath + "/StreamingAssets" + modelsFolder, modelName);
        string filePath = modelPath + modelsSuffix;

         // Debug.Log("CheckIfModelExist " + resFilePath);
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
    public static bool CheckIfModelExistinResources(string modelName)
    {
#if UNITY_WEBGL || UNITY_ANDROID
        //TODO: implement
        return true;
#else
        string resourcesPath = Path.Combine(Application.dataPath + "/Resources" + modelsFolder, modelName);
        string resFilePath = resourcesPath + modelsSuffix;
        Debug.Log("CheckIfModelExist " + resFilePath);
        if (File.Exists(resFilePath))
        {
            return true;
        }
        else
        {
            return File.Exists(resFilePath);
        }
#endif
    }

    public static IEnumerator ImportMaterial(string textureName, Action<Texture2D> callback)
    {
#if UNITY_WEBGL || UNITY_ANDROID //&& !UNITY_EDITOR
        yield return LoadTextureFromUrl(Application.streamingAssetsPath + texturesFolder + textureName + texturesSuffix, callback);
#else
        string texturePath = Application.dataPath + "/StreamingAssets" + texturesFolder + textureName + texturesSuffix;
        yield return LoadTexture(texturePath, callback);
#endif
    }

    public static IEnumerator ImportEnvironmentThumbnail(string textureName, Action<Texture2D> callback)
    {
#if UNITY_WEBGL || UNITY_ANDROID //&& !UNITY_EDITOR
        yield return LoadTextureFromUrl(Application.streamingAssetsPath + environmentFolder + textureName + texturesSuffix, callback);
#else
        string texturePath = Application.dataPath + "/StreamingAssets" + environmentFolder + textureName + texturesSuffix;
        yield return LoadTexture(texturePath, callback);
#endif
    }

    private static IEnumerator LoadTexture(string fullPath, Action<Texture2D> callback)
    {
        byte[] pngBytes = File.ReadAllBytes(fullPath);

        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(pngBytes);

        callback(tex);
        yield return null;
    }

    private static IEnumerator LoadTextureFromUrl(string fullPath, Action<Texture2D> callback)
    {
        Debug.Log("Load texture URL: " + fullPath);
        UnityWebRequest www = UnityWebRequest.Get(fullPath);

        yield return www.SendWebRequest();
        Texture2D tex = null;
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            tex = new Texture2D(2, 2);
            tex.LoadImage(www.downloadHandler.data);
            //tex = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
        callback(tex);
        yield return null;
    }

    public static IEnumerator ImportModel(string modelName, Action<GameObject> callback)
    {
#if UNITY_WEBGL //&& !UNITY_EDITOR
        string modelPath = appUrl + "StreamingAssets/" + modelName + modelsSuffix;
#elif UNITY_ANDROID
        string modelPath = Application.streamingAssetsPath + modelsFolder + modelName + modelsSuffix;
#else
        string modelPath = Application.dataPath + "/StreamingAssets" + modelsFolder + modelName + modelsSuffix;
#endif
        yield return LoadGLTF(modelPath, callback);
    }

    public static IEnumerator ImportBasicModel(string modelName, Action<GameObject> callback)
    {
        string modelPath = "Models/" + modelName;
        yield return LoadModelFromResources(modelPath, callback);
    }

    public static IEnumerator ImportEnvironment(string environmentName, Action<GameObject> callback)
    {
#if UNITY_WEBGL //&& !UNITY_EDITOR
        string modelPath = appUrl + "StreamingAssets/" + environmentFolder + environmentName + modelsSuffix;
#elif UNITY_ANDROID
        string modelPath = Application.streamingAssetsPath + environmentFolder + environmentName + modelsSuffix;
#else
        string modelPath = Application.dataPath + "/StreamingAssets" + environmentFolder + environmentName + modelsSuffix;
#endif
        yield return LoadGLTF(modelPath, callback);
    }

    private static IEnumerator LoadGLTF(string modelPath, Action<GameObject> callback)
    {
        GameObject loadedObject = null;
#if UNITY_WEBGL || UNITY_ANDROID //&& !UNITY_EDITOR
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
        loadedObject = Importer.LoadFromFile(modelPath);
#endif
        callback(loadedObject);
        yield return null;
    }

    private static IEnumerator LoadModelFromResources(string modelPath, Action<GameObject> callback)
    {
        GameObject loadedObject = null;
#if UNITY_WEBGL || UNITY_ANDROID //&& !UNITY_EDITOR

        //Debug.Log("LoadModelFromResources: " + modelPath);
        loadedObject = (GameObject)Resources.Load(modelPath);

#else
        loadedObject = (GameObject)Resources.Load(modelPath);
        //Debug.Log("loadedObject " + modelPath);
#endif
        callback(loadedObject);
        yield return null;
    }
}
