using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractFlat3D : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        CodeController codeController = GetComponent<CodeController>();
        if (codeController)
        {
            codeController.Interact();
        }
    }
}
