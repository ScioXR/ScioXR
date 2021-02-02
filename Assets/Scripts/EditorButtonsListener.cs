using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EditorButtonsListener : MonoBehaviour
{
    public XRCanvas toggleCanvas;

    // Update is called once per frame
    void Update()
    {
        if ((WebXRControllerDevice.leftHand != null && WebXRControllerDevice.leftHand.secondaryButton.wasPressedThisFrame) || Keyboard.current.tabKey.wasPressedThisFrame)
        {
            toggleCanvas.Toggle();
        }

        if ((WebXRControllerDevice.rightHand != null && WebXRControllerDevice.rightHand.secondaryButton.wasPressedThisFrame) || Keyboard.current.ctrlKey.wasPressedThisFrame)
        {
            EditorManager.instance.NextTransformMode();
        }
    }
}
