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

    public GameObject environment;
    public GameObject defaultEnvironment;

    [DllImport("__Internal")]
    private static extern void SyncFiles();

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
            if (AssetsLoader.CheckIfModelExist(dataJson.saveData[i].model))
            {
                Debug.Log("Model: " + dataJson.saveData[i].model + ", name " + dataJson.saveData[i].name + ", position " + dataJson.saveData[i].position + ", roation " + dataJson.saveData[i].rotation + ", scale " + dataJson.saveData[i].scale);
                //string modelPath = AssetsLoader.CheckIfModelExist(dataJson.saveData[i].model);
                SaveData currentData = dataJson.saveData[i];
                StartCoroutine(AssetsLoader.ImportModel(dataJson.saveData[i].model, loadedObject =>
                {
                    loadedObject.name = currentData.name;
                    loadedObject.transform.position = currentData.position;
                    loadedObject.transform.rotation = currentData.rotation;
                    loadedObject.transform.localScale = currentData.scale;
                    loadedObject.SetActive(currentData.isVisible == 1);

                    if (editor)
                    {
                        PlatformLoader.instance.platform.SetupEditorObject(loadedObject, currentData);

                        loadedObject.AddComponent<Saveable>();
                        loadedObject.GetComponent<Saveable>().model = currentData.model;
                        loadedObject.GetComponent<Saveable>().texture = currentData.texture;
                        loadedObject.GetComponent<Saveable>().color = currentData.color;
                        loadedObject.GetComponent<Saveable>().codeData = currentData.code;
                        loadedObject.GetComponent<Saveable>().isInteractable = currentData.isInteractable;
                        loadedObject.GetComponent<Saveable>().id = currentData.id;
                    } else
                    {
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
                            if (sceneObjects.id == currentData.parent)
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

                    objectToLoad--;
                    if (objectToLoad == 0)
                    {
                        AppManager.instance.loaded = true;
                        //LoadCode();
                    }
                }));                
            } else
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
        List<SaveData> saveDataList = GetSaveData();
        string filePath = sceneName;
        Debug.Log("SavingScene: " + filePath);
        using (var writer = new StreamWriter(File.Open(filePath, FileMode.Create)))
        {
            SaveData[] saveData = saveDataList.ToArray();
            SaveCollection saveCollection = new SaveCollection() { saveData = saveData, globalData = EditorManager.instance.globalData };

            string jsonString = JsonUtility.ToJson(saveCollection, true);
            writer.Write(jsonString);
        }
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            SyncFiles();
        }
    }

    private List<SaveData> GetSaveData()
    {
        List<SaveData> saveDataList = new List<SaveData>();
        Saveable[] saveableObjects = ObjectFinder.FindEvenInactiveComponents<Saveable>();
        for (int i = 0; i < saveableObjects.Length; i++)
        {
            if (saveableObjects[i].shouldSave)
            {
                SaveData dataStore = new SaveData()
                {
                    id = saveableObjects[i].id,
                    parent = saveableObjects[i].gameObject.transform.parent ? saveableObjects[i].gameObject.transform.parent.gameObject.GetComponent<Saveable>().id : 0,
                    name = saveableObjects[i].gameObject.name,
                    model = saveableObjects[i].model,
                    texture = saveableObjects[i].texture,
                    color = saveableObjects[i].color,
                    position = saveableObjects[i].transform.position,
                    rotation = saveableObjects[i].transform.rotation,
                    scale = saveableObjects[i].transform.localScale,
                    isVisible = saveableObjects[i].gameObject.activeInHierarchy ? 1 : 0,
                    isInteractable = saveableObjects[i].isInteractable,
                    code = saveableObjects[i].codeData,
                };
                saveDataList.Add(dataStore);
            }

        }
        return saveDataList;
    }

    public void SetEnvironment(string environmentName)
    {
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
