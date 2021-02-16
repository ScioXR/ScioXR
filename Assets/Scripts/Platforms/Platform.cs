using Siccity.GLTFUtility;
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

        SetInteractable(loadedModel, data.isInteractable);

        SetupGraphics(loadedModel, data);
    }

    public virtual void SetupEditorObject(GameObject loadedModel, SaveData data)
    {
        loadedModel.AddComponent<BoxCollider>();

        loadedModel.GetComponent<MeshRenderer>().material.shader = new ShaderSettings().GetDefaultMetallicBlend();

        Rigidbody rb = loadedModel.AddComponent<Rigidbody>();
        rb.isKinematic = true;

        SetupGraphics(loadedModel, data);
    }

    public virtual void SetInteractable(GameObject gameObject, bool interactable)
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if (interactable)
        {
            if (!rb)
            {
                gameObject.AddComponent<Rigidbody>();
            }
        } else
        {
            if (rb)
            {
                Destroy(rb);
            }
        }
    }

    public virtual bool IsEditModeSupported(TransformMode mode) {
        return true;
    }
}
