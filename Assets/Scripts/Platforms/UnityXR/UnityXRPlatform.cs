using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Management;

public class UnityXRPlatform : Platform
{
    public override bool Init()
    {
        XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError("Initializing XR Failed. Check Editor or Player log for details.");
            return false;
        }
        else
        {
            Debug.Log("Starting XR...");
            XRGeneralSettings.Instance.Manager.StartSubsystems();
        }
        return true;
    }

    public override void SetupEditorObject(GameObject loadedModel, string modelName)
    {
        base.SetupEditorObject(loadedModel, modelName);

        XRGrabInteractable grab = loadedModel.AddComponent<XRGrabInteractable>();
        EditorTransformXR editor = loadedModel.AddComponent<EditorTransformXR>();
    }
}
