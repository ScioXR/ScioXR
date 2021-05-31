﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebXREditorInteractable : MonoBehaviour, IWebXRInteractable
{
    private WebXRInteractor currentInteractor;
    private WebXRInteractor currentGrabInteractor;

    private Vector3 grabPositionOffset;
    private Quaternion grabRotationOffset;

    private float startDistanceFromCenter;
    private Vector3 startScale;

    private List<WebXREditorPivot> contactPivots = new List<WebXREditorPivot>();
    private WebXREditorPivot currentPivot;

    public GameObject gizmoScale;

    public GameObject followObject;

    private void Start()
    {
        gizmoScale.SetActive(false);
    }

    void IWebXRInteractable.OnGrab(WebXRInteractor interactor) {
        grabPositionOffset = interactor.gameObject.transform.position - transform.position;
        grabRotationOffset = interactor.gameObject.transform.rotation * Quaternion.Inverse(transform.rotation);
        currentGrabInteractor = interactor;
    }
    void IWebXRInteractable.OnUngrab(WebXRInteractor interactor) {
        currentGrabInteractor = null;
    }

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
        if (EditorManager.mode == TransformMode.CLONE)
        {
            GameObject clonedObject = Instantiate(gameObject, transform.position, transform.rotation);
            clonedObject.GetComponent<IWebXRInteractable>().OnGrab(interactor);
        }
    }

    void IWebXRInteractable.OnSecondaryUngrab(WebXRInteractor interactor)
    {
        currentInteractor = null;
        UnselectGizmo(null);
        gizmoScale.transform.SetParent(transform);
        if (EditorManager.mode == TransformMode.CLONE)
        {
        }
    }

    private void DoMove()
    {
        if (currentGrabInteractor)
        {
            transform.position = currentGrabInteractor.transform.position - grabPositionOffset;
            if (EditorSettings.instance.enableSnap)
            {
                if (EditorSettings.instance.snapMoveStep != 0)
                {
                    transform.position = new Vector3(transform.position.x - (transform.position.x % EditorSettings.instance.snapMoveStep), transform.position.y - (transform.position.y % EditorSettings.instance.snapMoveStep), transform.position.z - (transform.position.z % EditorSettings.instance.snapMoveStep));
                }
            }

            transform.rotation = currentGrabInteractor.transform.rotation * Quaternion.Inverse(grabRotationOffset);
            if (EditorSettings.instance.enableSnap)
            {
                if (EditorSettings.instance.snapRotateStep != 0)
                {
                    //transform.position = new Vector3(transform.position.x - (transform.position.x % EditorSettings.instance.snapMoveStep), transform.position.y - (transform.position.y % EditorSettings.instance.snapMoveStep), transform.position.z - (transform.position.z % EditorSettings.instance.snapMoveStep));
                }
            }
        }
    }

    private void DoDrag()
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
            DoMove();
            DoDrag();
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
