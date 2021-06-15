using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WebXREditorInteractable : MonoBehaviour, IWebXRInteractable
{
    private WebXRInteractor currentSecondaryGrabInteractor;
    private WebXRInteractor currentHighlightInteractor;
    private WebXRInteractor currentGrabInteractor;

    //private Vector3 grabPositionOffset;
    //private Quaternion grabRotationOffset;

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
        //grabPositionOffset = interactor.gameObject.transform.position - transform.position;
        //grabRotationOffset = interactor.gameObject.transform.rotation * Quaternion.Inverse(transform.rotation);
        currentGrabInteractor = interactor;
        interactor.attachTransform.position = transform.position;
        interactor.attachTransform.rotation = transform.rotation;
    }
    void IWebXRInteractable.OnUngrab(WebXRInteractor interactor) {
        currentGrabInteractor = null;
    }

    void IWebXRInteractable.OnSecondaryGrab(WebXRInteractor interactor)
    {
        currentSecondaryGrabInteractor = interactor;
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
        currentSecondaryGrabInteractor = null;
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
            //transform.position = currentGrabInteractor.transform.position - grabPositionOffset;
            transform.position = currentGrabInteractor.attachTransform.position;
            if (EditorSettings.instance.enableSnap)
            {
                if (EditorSettings.instance.snapMoveStep != 0)
                {
                    transform.position = new Vector3(transform.position.x - (transform.position.x % EditorSettings.instance.snapMoveStep), transform.position.y - (transform.position.y % EditorSettings.instance.snapMoveStep), transform.position.z - (transform.position.z % EditorSettings.instance.snapMoveStep));
                }
            }

            //transform.rotation = currentGrabInteractor.transform.rotation * Quaternion.Inverse(grabRotationOffset);
            Vector3 currentEulerRotation = transform.rotation.eulerAngles;
           
            if (EditorSettings.instance.enableSnap)
            {
                if (EditorSettings.instance.snapRotateStepX != 0)
                {
                    transform.eulerAngles = new Vector3(
                           currentGrabInteractor.attachTransform.rotation.eulerAngles.x - (currentGrabInteractor.attachTransform.rotation.eulerAngles.x % EditorSettings.instance.snapRotateStepX),
                            transform.eulerAngles.y,
                           transform.eulerAngles.z
                            );
                }
                if (EditorSettings.instance.snapRotateStepY != 0)
                {
                    transform.eulerAngles = new Vector3(
                           transform.eulerAngles.x,
                            currentGrabInteractor.attachTransform.rotation.eulerAngles.y - (currentGrabInteractor.attachTransform.rotation.eulerAngles.y % EditorSettings.instance.snapRotateStepY),
                           transform.eulerAngles.z
                            );
                }
                if (EditorSettings.instance.snapRotateStepZ != 0)
                {
                    transform.eulerAngles = new Vector3(
                           transform.eulerAngles.x,
                            transform.eulerAngles.x,
                           currentGrabInteractor.attachTransform.rotation.eulerAngles.z - (currentGrabInteractor.attachTransform.rotation.eulerAngles.z % EditorSettings.instance.snapRotateStepZ)
                            );
                }
            }
            else
            {
                transform.rotation = currentGrabInteractor.attachTransform.rotation;
            }                  
        }
    }

    private void DoDrag()
    {
        switch (EditorManager.mode)
        {
            case TransformMode.SCALE:
                Vector3 newScale = startScale;
                if (!EditorSettings.instance.enableSnap)
                {
                    float scaleFactor = ((transform.position - currentSecondaryGrabInteractor.transform.position).magnitude / startDistanceFromCenter);
                    if (!currentPivot || currentPivot.scaleMode == WebXREditorPivot.ScaleMode.ALL)
                    {
                        newScale *= scaleFactor;
                    }
                    else if (currentPivot.scaleMode == WebXREditorPivot.ScaleMode.X)
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
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentGrabInteractor)
        {
            DoMove();
        }
        if (currentSecondaryGrabInteractor)
        {
            DoDrag();
        }
    }


    void IWebXRInteractable.OnTouch(WebXRInteractor interactor)
    {
        GetComponent<Outline>().enabled = true;
        GetComponentsInChildren<Outline>().ToList().ForEach(outline => outline.enabled = true);
        currentHighlightInteractor = interactor;

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
        GetComponent<Outline>().enabled = false;
        GetComponentsInChildren<Outline>().ToList().ForEach(outline => outline.enabled = false);
        currentHighlightInteractor = null;

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
        if (currentSecondaryGrabInteractor)
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
