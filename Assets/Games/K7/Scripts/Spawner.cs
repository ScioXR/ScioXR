using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour, IWebXRInteractable
{
    public Outline outline;
    public GameObject prefab;

    void IWebXRInteractable.OnGrab(WebXRInteractor interactor)
    {
        
        GameObject spawnedObject = Instantiate(prefab, interactor.transform.position, Quaternion.identity);
        spawnedObject.GetComponent<IWebXRInteractable>().OnGrab(interactor);
    }

    void IWebXRInteractable.OnSecondaryGrab(WebXRInteractor interactor)
    {
        
    }

    void IWebXRInteractable.OnSecondaryUngrab(WebXRInteractor interactor)
    {
        
    }

    void IWebXRInteractable.OnTouch(WebXRInteractor interactor)
    {
        outline.enabled = true;
    }

    void IWebXRInteractable.OnUngrab(WebXRInteractor interactor)
    {
        
    }

    void IWebXRInteractable.OnUntouch(WebXRInteractor interactor)
    {
        outline.enabled = false;
    }
}
