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

    public override void SetupEditorObject(GameObject loadedModel, SaveData data)
    {
        base.SetupEditorObject(loadedModel, data);

        loadedModel.AddComponent<EditorTransform3D>();
        loadedModel.AddComponent<WebXREditorInteractable>();
        loadedModel.AddComponent<WebXRGrabInteractable>();

        var outline = loadedModel.AddComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = 5f;
        outline.enabled = false;
    }

    public override bool IsEditModeSupported(TransformMode mode)
    {
        return mode == TransformMode.NONE || mode == TransformMode.PROPERTIES || mode == TransformMode.SCALE || mode == TransformMode.CLONE;
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
