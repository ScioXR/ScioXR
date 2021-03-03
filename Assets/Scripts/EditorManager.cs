using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EditorManager : MonoBehaviour
{
    public KeyboardWebXR keyboard;

    public XRCanvas propertiesMenu;
    public XRCanvas mainMenu;

    public delegate void TransformModeChangedDelegate(TransformMode newValue);
    public static event TransformModeChangedDelegate OnTransformModeChanged;

    public static TransformMode mode;

    public static EditorManager instance;

    public GlobalData globalData;

    public GameObject selectedObject;

    public GameObject gizmoScalePrefab;

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

        if (PlatformLoader.instance.platform.IsEditModeSupported((TransformMode)newTransformMode))
        {
            SetTransformMode((TransformMode)newTransformMode);
        } else
        {
            EditorManager.mode = (TransformMode)newTransformMode;
            NextTransformMode();
        }
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
        if (IsPropertiesOpen())
        {
            
            //propertiesMenu.GetComponentInChildren<CodePanel>().LoadState();
            //load all data from game object
        }
    }

    public bool IsPropertiesOpen()
    {
        return propertiesMenu.IsActive();
    }

    public void DuplicateObject(GameObject cloneObject)
    {

        propertiesMenu.GetComponentInChildren<PropertiesPanel>().DuplicateButton(cloneObject);
    }

    public void ShowKeyboard(bool show, InputField textInput)
    {
        keyboard.textInput = textInput;
        string text = textInput ? textInput.text : "";
        ShowKeyboard(show, text);
    }

    public void ShowKeyboardTMP(bool show, TMP_InputField textInput)
    {
        keyboard.textInputTMP = textInput;
        string text = textInput ? textInput.text : "";
        ShowKeyboard(show, text);
    }

    private void ShowKeyboard(bool show, string text)
    {
        if (show)
        {
            keyboard.SetText(text);
            keyboard.Enable();
            keyboard.UpdatePosition();
        }
        else
        {
            keyboard.Disable();
            keyboard.textInput = null;
            keyboard.textInputTMP = null;
        }
    }
}
