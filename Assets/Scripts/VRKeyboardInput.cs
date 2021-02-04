using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VRKeyboardInput : MonoBehaviour, ISelectHandler, IDeselectHandler
{ 
    public void CloseKeybaord()
    {
        Debug.Log("CloseKeybaord");
        if (EditorManager.instance)
        {
            EditorManager.instance.ShowKeyboard(false, null);
        }
    }

    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        Debug.Log("OnSelect");
        EditorManager.instance.ShowKeyboard(true, GetComponent<InputField>());
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("OnDeselect");
        EditorManager.instance.ShowKeyboard(false, null);
    }
}
