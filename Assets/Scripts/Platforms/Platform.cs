using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public virtual bool Init()
    {
        return true;
    }

    public virtual void SetupPlayerObject(GameObject loadedModel)
    {
        loadedModel.AddComponent<BoxCollider>();

        Rigidbody rb = loadedModel.AddComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    public virtual void SetupEditorObject(GameObject loadedModel, string modelName)
    {
        loadedModel.AddComponent<BoxCollider>();

        Rigidbody rb = loadedModel.AddComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    public virtual bool IsEditModeSupported(TransformMode mode) {
        return true;
    }
}
