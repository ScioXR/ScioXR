using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public virtual bool Init()
    {
        return true;
    }

    protected virtual void SetupGraphics(GameObject loadedModel, SaveData data)
    {
        if (data.texture != "")
        {
            StartCoroutine(AssetsLoader.ImportTexture(data.texture, texture =>
            {
                loadedModel.GetComponent<MeshRenderer>().material.mainTexture = texture;
            }));
        }
    }

    public virtual void SetupPlayerObject(GameObject loadedModel, SaveData data)
    {
        loadedModel.AddComponent<BoxCollider>();

        if (data.isInteractable)
        {
            Rigidbody rb = loadedModel.AddComponent<Rigidbody>();
        }

        SetupGraphics(loadedModel, data);
    }

    public virtual void SetupEditorObject(GameObject loadedModel, SaveData data)
    {
        loadedModel.AddComponent<BoxCollider>();

        Rigidbody rb = loadedModel.AddComponent<Rigidbody>();
        rb.isKinematic = true;

        SetupGraphics(loadedModel, data);
    }

    public virtual bool IsEditModeSupported(TransformMode mode) {
        return true;
    }
}
