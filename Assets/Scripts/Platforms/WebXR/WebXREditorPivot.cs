using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebXREditorPivot : MonoBehaviour
{
    public MeshRenderer[] objectsToHighlight;
    public WebXRInteractor currentInteractor;

    private WebXREditorInteractable editorInteractable;

    public enum ScaleMode
    {
        ALL,
        X,
        Y,
        Z
    }

    public ScaleMode scaleMode;

    private void Start()
    {
        editorInteractable = GetComponentInParent<WebXREditorInteractable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<WebXRInteractor>())
        {
            //Debug.Log("Select: " + gameObject.name);
            editorInteractable.SelectGizmo(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponentInParent<WebXRInteractor>())
        {
            //Debug.Log("Deselect: " + gameObject.name);
            editorInteractable.UnselectGizmo(this);
        }
    }

    public void Highlight(bool shouldHighlight)
    {
        foreach (MeshRenderer highlight in objectsToHighlight)
        {
            highlight.material.color = shouldHighlight ? Color.green : Color.white;
        }
    }
}
