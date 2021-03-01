using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebXREditorInteractable : MonoBehaviour, IWebXRInteractable
{
    private WebXRInteractor currentInteractor;

    private float startDistanceFromCenter;
    private Vector3 startScale;

    private List<WebXREditorPivot> contactPivots = new List<WebXREditorPivot>();
    private WebXREditorPivot currentPivot;

    public GameObject gizmoScale;

    private void Start()
    {
        gizmoScale.SetActive(false);
    }

    void IWebXRInteractable.OnGrab(WebXRInteractor interactor) { }
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
            gizmoScale.transform.SetParent(null);
            startDistanceFromCenter = (transform.position - interactor.transform.position).magnitude;
            startScale = transform.localScale;
        }
        if (EditorManager.mode == TransformMode.SET_PARENT)
        {
            if (EditorManager.instance.selectedObject == gameObject)
            {
                EditorManager.instance.selectedObject.transform.SetParent(null);
            }
            else
            {
                EditorManager.instance.selectedObject.transform.SetParent(transform);
            }
        }
    }

    void IWebXRInteractable.OnSecondaryUngrab(WebXRInteractor interactor)
    {
        currentInteractor = null;
        UnselectGizmo(null);
        gizmoScale.transform.SetParent(transform);
    }

    private void Dragging()
    {
        switch (EditorManager.mode)
        {
            case TransformMode.SCALE:
                Vector3 newScale = startScale;
                float scaleFactor = ((transform.position - currentInteractor.transform.position).magnitude / startDistanceFromCenter);
                if (!currentPivot || currentPivot.scaleMode == WebXREditorPivot.ScaleMode.ALL)
                {
                    newScale *= scaleFactor;
                } else if (currentPivot.scaleMode == WebXREditorPivot.ScaleMode.X)
                {
                    newScale.x *= scaleFactor;
                }
                else if (currentPivot.scaleMode == WebXREditorPivot.ScaleMode.Y)
                {
                    newScale.y *= scaleFactor;
                }
                else if (currentPivot.scaleMode == WebXREditorPivot.ScaleMode.Z)
                {
                    newScale.z *= scaleFactor;
                }
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
        if (EditorManager.mode == TransformMode.SCALE)
        {
            gizmoScale.SetActive(true);
            Color color = GetComponent<MeshRenderer>().material.color;
            color.a = 0.4f;
            GetComponent<MeshRenderer>().material.color = color;
        }
    }

    void IWebXRInteractable.OnUntouch(WebXRInteractor interactor)
    {
        if (EditorManager.mode == TransformMode.SCALE)
        {
            Color color = GetComponent<MeshRenderer>().material.color;
            color.a = 1;
            GetComponent<MeshRenderer>().material.color = color;
            gizmoScale.SetActive(false);
        }
    }
    
    public void SelectGizmo(WebXREditorPivot selectedPivot)
    {
        if (currentInteractor)
        {
            return;
        }

        if (currentPivot != selectedPivot)
        {
            
            if (currentPivot)
            {
                currentPivot.Highlight(false);
            }
            currentPivot = selectedPivot;
            if (currentPivot)
            {
                currentPivot.Highlight(true);
                contactPivots.Add(currentPivot);
            }
        }
    }

    public void UnselectGizmo(WebXREditorPivot unselectedPivot)
    {
        contactPivots.Remove(unselectedPivot);
        if (currentPivot == unselectedPivot)
        {
            SelectGizmo(contactPivots.Count > 0 ? contactPivots[contactPivots.Count - 1] : null);
        }
    }
}
