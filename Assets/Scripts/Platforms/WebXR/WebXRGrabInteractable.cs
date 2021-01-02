using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebXRGrabInteractable : MonoBehaviour, IWebXRInteractable
{
    private Rigidbody rb;

    private WebXRInteractor currentInteractor;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnDestroy()
    {
        if (currentInteractor)
        {
            currentInteractor.ForceUntouchObject(rb);
        }
    }

    void IWebXRInteractable.OnGrab(WebXRInteractor interactor)
    {
        rb.MovePosition(interactor.transform.position);
        rb.isKinematic = false;
        interactor.attachJoint.connectedBody = rb;
    }

    void IWebXRInteractable.OnUngrab(WebXRInteractor interactor)
    {
        interactor.attachJoint.connectedBody = null;
        rb.isKinematic = true;
    }

    void IWebXRInteractable.OnSecondaryGrab(WebXRInteractor interactor) {}

    void IWebXRInteractable.OnSecondaryUngrab(WebXRInteractor interactor) {}

    void IWebXRInteractable.OnTouch(WebXRInteractor interactor)
    {
        GetComponent<Outline>().enabled = true;
        currentInteractor = interactor;
    }

    void IWebXRInteractable.OnUntouch(WebXRInteractor interactor)
    {
        GetComponent<Outline>().enabled = false;
        currentInteractor = null;
    }
}
