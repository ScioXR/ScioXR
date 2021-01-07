using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebXRGrabInteractable : MonoBehaviour, IWebXRInteractable
{
    private Rigidbody rb;

    private WebXRInteractor currentInteractor;

    private bool baseKinematic;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        baseKinematic = rb.isKinematic;
    }

    private void OnDestroy()
    {
        if (currentInteractor)
        {
            currentInteractor.ForceUntouchObject(rb);
        }
    }

    public void ForceUngrab()
    {
        if (currentInteractor)
        {
            currentInteractor.attachJoint.connectedBody = null;
            rb.isKinematic = baseKinematic;

            currentInteractor.ForceUntouchObject(rb);

            GetComponent<Outline>().enabled = false;
            currentInteractor = null;
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
        //Debug.Log("OnUngrab");
        interactor.attachJoint.connectedBody = null;
        rb.isKinematic = baseKinematic;
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
