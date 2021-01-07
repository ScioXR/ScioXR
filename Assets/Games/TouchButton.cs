using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TouchButton : MonoBehaviour, IWebXRInteractable
{
    private Color baseColor;

    public bool touchColorChange;
    public Color touchColor;
    public float touchColorDelay;
    public UnityEvent onTouch;

    private void Start()
    {
        baseColor = GetComponent<MeshRenderer>().material.color;
    }

    void IWebXRInteractable.OnGrab(WebXRInteractor interactor)
    {
    
    }

    void IWebXRInteractable.OnSecondaryGrab(WebXRInteractor interactor)
    {

    }

    void IWebXRInteractable.OnSecondaryUngrab(WebXRInteractor interactor)
    {

    }

    void IWebXRInteractable.OnTouch(WebXRInteractor interactor)
    {
        onTouch.Invoke();
        if(touchColorChange)
        {
            GetComponent<MeshRenderer>().material.color = touchColor;
            CancelInvoke();
            Invoke("ResetColor", touchColorDelay);
        }
    }

    void IWebXRInteractable.OnUngrab(WebXRInteractor interactor)
    {

    }

    void IWebXRInteractable.OnUntouch(WebXRInteractor interactor)
    {

    }

    public void ResetColor()
    {
        GetComponent<MeshRenderer>().material.color = baseColor;
    }
}
