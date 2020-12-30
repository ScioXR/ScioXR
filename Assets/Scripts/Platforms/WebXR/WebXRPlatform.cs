using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebXR;

public class WebXRPlatform : Platform
{

    private void Start()
    {
        WebXRManager.OnXRChange += onXRChange;
    }

    public override void SetupEditorObject(GameObject loadedModel, string modelName)
    {
        base.SetupEditorObject(loadedModel, modelName);

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
    }
}
