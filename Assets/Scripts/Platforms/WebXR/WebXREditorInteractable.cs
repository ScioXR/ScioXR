using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebXREditorInteractable : MonoBehaviour, IWebXRInteractable
{
    private WebXRInteractor currentInteractor;

    private float startDistanceFromCenter;
    private Vector3 startScale;

    void IWebXRInteractable.OnGrab(WebXRInteractor interactor) {}
    void IWebXRInteractable.OnUngrab(WebXRInteractor interactor) { }

    void IWebXRInteractable.OnSecondaryGrab(WebXRInteractor interactor)
    {
        currentInteractor = interactor;
        if (EditorManager.mode == TransformMode.PROPERTIES)
        {
            EditorManager.instance.ToggleProperties(gameObject);
        }
        if (EditorManager.mode == TransformMode.SCALE)
        {
            startDistanceFromCenter = (transform.position - interactor.transform.position).magnitude;
            startScale = transform.localScale;
        }
    }

    void IWebXRInteractable.OnSecondaryUngrab(WebXRInteractor interactor)
    {
        currentInteractor = null;
    }

    private void Dragging()
    {
        switch (EditorManager.mode)
        {
            case TransformMode.SCALE:
                Vector3 newScale = startScale * ((transform.position - currentInteractor.transform.position).magnitude / startDistanceFromCenter);
                transform.localScale = newScale;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentInteractor)
        {
            Dragging();
        }
    }

    void IWebXRInteractable.OnTouch(WebXRInteractor interactor)
    {
    }

    void IWebXRInteractable.OnUntouch(WebXRInteractor interactor)
    {
    }
}
