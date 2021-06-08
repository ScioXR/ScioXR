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

    protected virtual void SetupGraphics(GameObject loadedModel, ObjectData data)
    {
        loadedModel.GetComponent<Saveable>().SetupGraphics();
    }

    public virtual void SetupPlayerObject(GameObject loadedModel, ObjectData data)
    {
        loadedModel.AddComponent<BoxCollider>();

        SetInteractable(loadedModel, data.isInteractable);

        SetupGraphics(loadedModel, data);
    }

    public virtual void SetupEditorObject(GameObject loadedModel, ObjectData data)
    {
        loadedModel.AddComponent<BoxCollider>();

        loadedModel.GetComponent<MeshRenderer>().material.shader = new ShaderSettings().GetDefaultMetallicBlend();

        Rigidbody rb = loadedModel.AddComponent<Rigidbody>();
        rb.isKinematic = true;

        var outline = loadedModel.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = 5f;
        outline.enabled = false;

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

                var outline = gameObject.AddComponent<Outline>();
                outline.OutlineMode = Outline.Mode.OutlineAll;
                outline.OutlineColor = Color.yellow;
                outline.OutlineWidth = 5f;
                outline.enabled = false;
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
