using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorButtonsListener : MonoBehaviour
{
    public XRCanvas toggleCanvas;

    // Update is called once per frame
    void Update()
    {
        if (WebXRControllerDevice.leftHand.secondaryButton.wasPressedThisFrame)
        {
            toggleCanvas.Toggle();
        }

        if (WebXRControllerDevice.rightHand.secondaryButton.wasPressedThisFrame)
        {
            EditorManager.instance.NextTransformMode();
        }
    }
}
