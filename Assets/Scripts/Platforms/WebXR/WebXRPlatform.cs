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
    }

    private void onXRChange(WebXRState state, int viewsCount, Rect leftRect, Rect rightRect)
    {
        CameraHelper.Reset();
    }
}
