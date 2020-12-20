using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.XR.Interaction.Toolkit.UI;
using WebXR;

public class WebXRContollerVirtualDevice : MonoBehaviour
{
    private WebXRControllerDevice device;

    private WebXRController controller;

    private WebXRControllerDeviceState cachedDeviceState = new WebXRControllerDeviceState();

    private void Awake()
    {
        controller = gameObject.GetComponent<WebXRController>();
    }

    private void OnEnable()
    {
        SetupDevice();
    }

    private void OnDestroy()
    {
        InputSystem.RemoveDevice(device);
    }

    private void SetupDevice()
    {
        InternedString usage = controller.hand == WebXRControllerHand.LEFT ? CommonUsages.LeftHand : CommonUsages.RightHand;

        device = InputSystem.GetDevice<WebXRControllerDevice>(usage);

        if (device == null)
        {
            device = InputSystem.AddDevice<WebXRControllerDevice>("WebXRController:" + usage);            
            InputSystem.AddDeviceUsage(device, usage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        controller.TryUpdateButtons();

        bool trigger = controller.GetButton(WebXRController.ButtonTypes.Trigger);
        bool triggerDown = controller.GetButtonDown(WebXRController.ButtonTypes.Trigger);
        bool triggerUp = controller.GetButtonUp(WebXRController.ButtonTypes.Trigger);
        bool grip = controller.GetButton(WebXRController.ButtonTypes.Grip);
        bool gripDown = controller.GetButtonDown(WebXRController.ButtonTypes.Grip);
        bool gripUp = controller.GetButtonUp(WebXRController.ButtonTypes.Grip);
        //bool aButtonDown = controller.GetButtonDown(WebXRController.ButtonTypes.ButtonA);

        float triggerValue = controller.GetAxis(WebXRController.AxisTypes.Trigger);
        cachedDeviceState.trigger = triggerValue;

        if (triggerDown || triggerUp || gripDown || gripUp)
        {
            int buttons = (trigger ? 1 : 0) | (grip ? 1 << 1 : 0);
            cachedDeviceState.buttons = buttons;
        }
        InputSystem.QueueStateEvent(device, cachedDeviceState);
    }
}
