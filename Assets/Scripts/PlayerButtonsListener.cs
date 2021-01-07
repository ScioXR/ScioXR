using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerButtonsListener : MonoBehaviour
{
    public XRCanvas toggleCanvas;

    // Update is called once per frame
    void Update()
    {
        bool togglePress = false;
        togglePress |= WebXRControllerDevice.leftHand != null && WebXRControllerDevice.leftHand.secondaryButton.wasPressedThisFrame;
        togglePress |= Keyboard.current.tabKey.wasPressedThisFrame;
        if (togglePress)
        {
            toggleCanvas.Toggle();
        }
    }
}
