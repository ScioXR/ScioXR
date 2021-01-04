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
using WebXR;

public class WebXRContollerVirtualDevice : MonoBehaviour
{
    private WebXRControllerDevice device;

    private WebXRController controller;

    private WebXRControllerDeviceState prevCachedDeviceState = new WebXRControllerDeviceState();
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
        cachedDeviceState.thumbstick = controller.GetAxis2D(WebXRController.Axis2DTypes.Thumbstick);
        cachedDeviceState.trigger = controller.GetAxis(WebXRController.AxisTypes.Trigger);
        cachedDeviceState.buttons = (controller.GetButton(WebXRController.ButtonTypes.Trigger) ? 1 : 0) |
                                    (controller.GetButton(WebXRController.ButtonTypes.Grip) ? 1 : 0) << 1 |
                                    (controller.GetButton(WebXRController.ButtonTypes.ButtonA) ? 1 : 0) << 2 |
                                    (controller.GetButton(WebXRController.ButtonTypes.ButtonB) ? 1 : 0) << 3;

        if (cachedDeviceState != prevCachedDeviceState)
        {
            InputSystem.QueueStateEvent(device, cachedDeviceState);
        }
        

        prevCachedDeviceState.CopyFrom(cachedDeviceState);
    }
}
