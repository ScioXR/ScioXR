using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebXR;

public class WebXRPlatform : Platform
{
    public Camera trackCameraMain;
    public Camera trackCameraVR;
    public Camera trackCameraAR;

    private void Start()
    {
        WebXRManager.OnXRChange += onXRChange;
    }

    public override void SetupPlayerObject(GameObject loadedModel, ObjectData data)
    {
        base.SetupPlayerObject(loadedModel, data);

        if (data.isInteractable)
        {
            loadedModel.AddComponent<WebXRGrabInteractable>();
        }
    }

    public override void SetupEditorObject(GameObject loadedModel, ObjectData data)
    {
        base.SetupEditorObject(loadedModel, data);

        loadedModel.AddComponent<EditorTransform3D>();
        loadedModel.AddComponent<WebXREditorInteractable>();
        loadedModel.AddComponent<WebXRGrabInteractable>();

        loadedModel.GetComponent<WebXREditorInteractable>().gizmoScale = Instantiate(EditorManager.instance.gizmoScalePrefab, loadedModel.transform);
    }

    public override void SetInteractable(GameObject gameObject, bool interactable)
    {
        base.SetInteractable(gameObject, interactable);

        WebXRGrabInteractable grab = gameObject.GetComponent<WebXRGrabInteractable>();
        if (interactable)
        {
            if (!grab)
            {
                gameObject.AddComponent<WebXRGrabInteractable>();
            }
        }
        else
        {
            if (grab)
            {
                Destroy(grab);
            }
        }
    }

    public override bool IsEditModeSupported(TransformMode mode)
    {
        return mode == TransformMode.NONE 
            || mode == TransformMode.PROPERTIES 
            || mode == TransformMode.SCALE 
            || mode == TransformMode.CLONE;
    }

    private void onXRChange(WebXRState state, int viewsCount, Rect leftRect, Rect rightRect)
    {
        CameraHelper.Reset();
        if (state == WebXRState.VR)
        {
            CameraHelper.SetCamera(trackCameraVR);
        }
        else if (state == WebXRState.AR)
        {
            CameraHelper.SetCamera(trackCameraAR);
        }
    }
}
