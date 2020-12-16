using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EditorManager : MonoBehaviour
{
    public XRCanvas propertiesMenu;
    public XRCanvas mainMenu;

    public delegate void TransformModeChangedDelegate(TransformMode newValue);
    public static event TransformModeChangedDelegate OnTransformModeChanged;

    public static TransformMode mode;

    public static EditorManager instance;

    public GlobalData globalData;

    public GameObject selectedObject;

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

    private void Start()
    {
        SetTransformMode(mode);

        if (AppManager.instance.currentSceneName != "")
        {
            //Debug.Log("Loading: " + AppManager.instance.currentSceneName);
            SaveCollection dataJson = ScioXRSceneManager.instance.LoadFromJson(AppManager.instance.GetScenePath());
            ScioXRSceneManager.instance.CreateLoadedObjects(dataJson, true);
        }
    }

    public void NextTransformMode(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            NextTransformMode();
        }
    }

    public void NextTransformMode()
    {
        //Debug.Log("NextTransformMode: " + mode);
        int newTransformMode = (int)mode + 1;
        if (newTransformMode >= Enum.GetValues(typeof(TransformMode)).Length)
        {
            newTransformMode = 0;
        }
        SetTransformMode((TransformMode)newTransformMode);
    }

    public void SetTransformMode(TransformMode mode)
    {
        EditorManager.mode = mode;
        OnTransformModeChanged(mode);
    }

    public void ToggleProperties(GameObject editObject)
    {
        selectedObject = editObject;
        propertiesMenu.Toggle();
        if (propertiesMenu.IsActive())
        {
            
            //propertiesMenu.GetComponentInChildren<CodePanel>().LoadState();
            //load all data from game object
        }
    }
    public void DuplicateObject(GameObject cloneObject)
    {
        propertiesMenu.GetComponentInChildren<PropertiesPanel>().DuplicateButton(cloneObject);
    }
}
