using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

public class ScioXRSceneManager : MonoBehaviour
{
    static string scenesSufix = ".json";

    public static ScioXRSceneManager instance;

    public string environmentName;
    public GameObject environment;
    public GameObject defaultEnvironment;

#if UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern void SyncFiles();
#endif

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

    public List<string> GetScenesList(string path)
    {
        List<string> scenesName = new List<string>();
        string assetsPath = path;
        string[] modelEntries = Directory.GetFiles(assetsPath);
        for (int i = 0; i < modelEntries.Length; i++)
        {
            if (!modelEntries[i].Contains("meta") && modelEntries[i].EndsWith(scenesSufix))
            { // Debug.Log("GetModelsList " + fileEntries[i]);
                string fileName = modelEntries[i].Replace('\\', '/');

                int index = fileName.LastIndexOf("/");
                string name = fileName.Remove(0, index + 1);
                if (index >= 0)
                {
                    int stringSufix = name.LastIndexOf(".");
                    string cleanName = name.Remove(stringSufix);
                    scenesName.Add(cleanName);
                    // Debug.Log("file " + clearName);
                }
            }
        }
        return scenesName;
    }

    //load from json if success if invalid
    public SaveCollection LoadFromJson(string sceneName)
    {
        Debug.Log("LoadFromJson: " + sceneName);
        string filePath = sceneName;

        if (File.Exists(filePath))
        {
            string data = File.ReadAllText(filePath);
            // Debug.Log("Data: " + data);           
            try
            {
                SaveCollection dataJson = JsonUtility.FromJson<SaveCollection>(data);
                return dataJson;

            }
            catch (FormatException fex)
            {
                //Invalid json format
                Debug.LogError("FormatException: " + fex);
            }
            catch (Exception ex)
            {
                //some other exception
                Debug.LogError("Exception: " + ex);
            }
        }
        else
        {
            Debug.LogError("There is no SaveData!");
        }
        return null;
    }

    public void CreateLoadedObjects(SaveCollection dataJson, bool editor)
    {
        int objectToLoad = dataJson.saveData.Length;

        //creat variables and messages
        if (editor)
        {
            EditorManager.instance.globalData = dataJson.globalData;
        }

        for (int i = 0; i < dataJson.saveData.Length; i++)
        {
            if (AssetsLoader.CheckIfModelExistinResources(dataJson.saveData[i].model))
            {
                Debug.Log("Model Basic: " + dataJson.saveData[i].model + ", name " + dataJson.saveData[i].name + ", position " + dataJson.saveData[i].position + ", roation " + dataJson.saveData[i].rotation + ", scale " + dataJson.saveData[i].scale);
                //string modelPath = AssetsLoader.CheckIfModelExist(dataJson.saveData[i].model);
                ObjectData currentData = dataJson.saveData[i];
                StartCoroutine(AssetsLoader.ImportBasicModel(dataJson.saveData[i].model, loadedObject =>
                {
                    loadedObject = Instantiate(loadedObject, loadedObject.transform.parent, true) as GameObject;
                    CreateLoadedObject(loadedObject, currentData, editor);
                    objectToLoad--;
                    if (objectToLoad == 0)
                    {
                        AppManager.instance.loaded = true;
                    }
                }));
            }
            else if (AssetsLoader.CheckIfModelExist(dataJson.saveData[i].model))
            {
                Debug.Log("Model: " + dataJson.saveData[i].model + ", name " + dataJson.saveData[i].name + ", position " + dataJson.saveData[i].position + ", roation " + dataJson.saveData[i].rotation + ", scale " + dataJson.saveData[i].scale);
                //string modelPath = AssetsLoader.CheckIfModelExist(dataJson.saveData[i].model);
                ObjectData currentData = dataJson.saveData[i];
                StartCoroutine(AssetsLoader.ImportModel(dataJson.saveData[i].model, loadedObject =>
                {
                    CreateLoadedObject(loadedObject, currentData, editor);
                    objectToLoad--;
                    if (objectToLoad == 0)
                    {
                        AppManager.instance.loaded = true;
                    }
                }));
            }  
            else
            {
                objectToLoad--;
                if (objectToLoad == 0)
                {
                    AppManager.instance.loaded = true;
                    //PlayerManager.instance.started = true;
                }
            }
        }
    }

    public void CreateLoadedObject(GameObject loadedObject, ObjectData currentData, bool editor)
    {
        loadedObject.name = currentData.name;
        loadedObject.transform.position = currentData.position;
        loadedObject.transform.rotation = currentData.rotation;
        loadedObject.transform.localScale = currentData.scale;
        loadedObject.SetActive(currentData.isVisible == 1);

        if (editor)
        {
            loadedObject.AddComponent<Saveable>().data = currentData;
            PlatformLoader.instance.platform.SetupEditorObject(loadedObject, currentData);


        }
        else
        {
            loadedObject.AddComponent<Saveable>().data = currentData;
            PlatformLoader.instance.platform.SetupPlayerObject(loadedObject, currentData);

            loadedObject.AddComponent<CodeController>();
            loadedObject.GetComponent<CodeController>().id = currentData.id;
            loadedObject.GetComponent<CodeController>().LoadCode(currentData.code);
        }

        //link parent
        if (currentData.parent > 0)
        {
            bool parentFound = false;
            Saveable[] allObjects = GameObject.FindObjectsOfType<Saveable>();
            foreach (var sceneObjects in allObjects)
            {
                if (sceneObjects.data.id == currentData.parent)
                {
                    parentFound = true;
                    loadedObject.transform.SetParent(sceneObjects.gameObject.transform);
                    loadedObject.transform.localScale = currentData.scale;
                    break;
                }
            }
            if (!parentFound)
            {
                Debug.LogError("Cannot find parent for " + currentData.id);
            }
        }
    }

    public GameObject GetObject(int objectId)
    {
        CodeController[] codeControllers = FindObjectsOfType<CodeController>();
        foreach (var codeController in codeControllers)
        {
            if (codeController.id == objectId)
            {
                return codeController.gameObject;
            }
        }
        return null;
    }

    public void SaveScene(string sceneName)
    {
        List<ObjectData> saveDataList = GetSaveData();
        string filePath = sceneName;
        Debug.Log("SavingScene: " + filePath);
        using (var writer = new StreamWriter(File.Open(filePath, FileMode.Create)))
        {
            ObjectData[] saveData = saveDataList.ToArray();
            SaveCollection saveCollection = new SaveCollection() { saveData = saveData, globalData = EditorManager.instance.globalData, environment = environmentName };

            string jsonString = JsonUtility.ToJson(saveCollection, true);
            writer.Write(jsonString);
        }
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
#if UNITY_WEBGL
            SyncFiles();
#endif
        }
    }

    private List<ObjectData> GetSaveData()
    {
        List<ObjectData> saveDataList = new List<ObjectData>();
        Saveable[] saveableObjects = ObjectFinder.FindEvenInactiveComponents<Saveable>();
        for (int i = 0; i < saveableObjects.Length; i++)
        {
            if (saveableObjects[i].shouldSave)
            {
                Debug.Log("GetSaveData " + saveableObjects[i].name);
                saveDataList.Add(saveableObjects[i].GetSavableData());
            }

        }
        return saveDataList;
    }

    public void SetEnvironment(string environmentName)
    {
        if (environmentName != null)
        {
            this.environmentName = environmentName;
            defaultEnvironment.SetActive(true);
            if (environment)
            {
                Destroy(environment);
                environment = null;
            }
            if (environmentName != "")
            {
                StartCoroutine(AssetsLoader.ImportEnvironment(environmentName, importedObject =>
                {
                    defaultEnvironment.SetActive(false);
                    environment = importedObject;
                }));
            }
        }
    }
}
