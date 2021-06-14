using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WebXRInteract : MonoBehaviour, IWebXRInteractable, IPointerClickHandler
{
    public void OnGrab(WebXRInteractor interactor)
    {
       
    }

    public void OnSecondaryGrab(WebXRInteractor interactor)
    {
       
    }

    public void OnSecondaryUngrab(WebXRInteractor interactor)
    {
        
    }

    public void OnTouch(WebXRInteractor interactor)
    {
        CodeController codeController = GetComponent<CodeController>();
        if (codeController)
        {
            codeController.Interact();
        }
    }

    public void OnUngrab(WebXRInteractor interactor)
    {
       
    }

    public void OnUntouch(WebXRInteractor interactor)
    {
       
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CodeController codeController = GetComponent<CodeController>();
        if (codeController)
        {
            codeController.Interact();
        }
    }
}
