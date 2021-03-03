using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VRKeyboardInput : MonoBehaviour, ISelectHandler, IDeselectHandler
{ 
    public void CloseKeybaord()
    {
        if (EditorManager.instance)
        {
            EditorManager.instance.ShowKeyboard(false, null);
        }
    }

    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        if (GetComponent<InputField>())
        {
            EditorManager.instance.ShowKeyboard(true, GetComponent<InputField>());
        }
        else if (GetComponent<TMP_InputField>())
        {
            EditorManager.instance.ShowKeyboardTMP(true, GetComponent<TMP_InputField>());
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        EditorManager.instance.ShowKeyboard(false, null);
    }
}
