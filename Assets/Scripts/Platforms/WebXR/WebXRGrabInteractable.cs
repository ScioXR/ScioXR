using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebXRGrabInteractable : MonoBehaviour, IWebXRInteractable
{
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
}
