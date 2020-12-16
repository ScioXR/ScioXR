using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scio3DPlatform : Platform
{
    public override void SetupPlayerObject(GameObject loadedModel)
    {
        base.SetupPlayerObject(loadedModel);

        loadedModel.AddComponent<InteractFlat3D>();
    }

    public override void SetupEditorObject(GameObject loadedModel, string modelName)
    {
        base.SetupEditorObject(loadedModel, modelName);

        loadedModel.AddComponent<EditorTransform3D>();
    }
}
