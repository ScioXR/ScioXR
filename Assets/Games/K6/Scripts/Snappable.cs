using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snappable : MonoBehaviour, IWebXRInteractable
{
    private Rigidbody rb;

    public SnapPlace snapPlace;

    public bool ignoreNextExit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void IWebXRInteractable.OnGrab(WebXRInteractor interactor)
    {
        if (snapPlace)
        {
            ignoreNextExit = true;
        }
    }

    void IWebXRInteractable.OnSecondaryGrab(WebXRInteractor interactor)
    {

    }

    void IWebXRInteractable.OnSecondaryUngrab(WebXRInteractor interactor)
    {

    }

    void IWebXRInteractable.OnTouch(WebXRInteractor interactor)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!snapPlace && other.gameObject.GetComponent<SnapPlace>() && other.gameObject.GetComponent<SnapPlace>().CanSnap())
        {
            Debug.Log("Snap: " + name + ", " + other.gameObject.name);
            GetComponent<WebXRGrabInteractable>().ForceUngrab();
            Snap(other.gameObject.GetComponent<SnapPlace>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
       
        if (snapPlace && snapPlace == other.gameObject.GetComponent<SnapPlace>())
        {
            if (ignoreNextExit)
            {
                Debug.Log("ignoreNextExit: " + other.gameObject);
                ignoreNextExit = false;
            }
            else
            {
                Debug.Log("Unsnap: " + name + ", " + other.gameObject.name);
                Unsnapp();
            }
        }
    }

    void IWebXRInteractable.OnUngrab(WebXRInteractor interactor)
    {
        Debug.Log("Snappable OnUngrab");
        if (snapPlace)
        {
            Snap(snapPlace);
        }
    }

    void IWebXRInteractable.OnUntouch(WebXRInteractor interactor)
    {

    }

    public void Snap(SnapPlace newSnapPlace)
    {
        ignoreNextExit = true;

        newSnapPlace.snappedObject = gameObject;
        snapPlace = newSnapPlace;
        transform.position = snapPlace.transform.position;
        transform.rotation = snapPlace.transform.rotation;

        rb.isKinematic = true;
    }

    public void Unsnapp()
    {
        snapPlace.snappedObject = null;
        snapPlace = null;
    }
}
