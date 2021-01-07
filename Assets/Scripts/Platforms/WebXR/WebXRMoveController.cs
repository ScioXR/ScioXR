using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using WebXR;

public class WebXRMoveController : MonoBehaviour
{
    public float walkingSpeed = 1.0f;

    WebXRControllerDevice controllerDevice;

    public WebXRControllerHand hand;

    // Start is called before the first frame update
    void Start()
    {
        InternedString usage = hand == WebXRControllerHand.LEFT ? CommonUsages.LeftHand : CommonUsages.RightHand;
        controllerDevice = InputSystem.GetDevice<WebXRControllerDevice>(usage);
    }

    void Update()
    {
        if (CameraHelper.main)
        {
            Vector3 forward = CameraHelper.main.transform.TransformDirection(Vector3.forward);
            forward.y = 0;
            Vector3 right = CameraHelper.main.transform.TransformDirection(Vector3.right);
            right.y = 0;

            float thumbstickX = controllerDevice.thumbstick.x.ReadValue();
            float thumbstickY = controllerDevice.thumbstick.y.ReadValue();

            if (thumbstickY != 0)
            {
                transform.position += forward * walkingSpeed * thumbstickY * Time.deltaTime;
            }
            if (thumbstickX != 0)
            {
                transform.position += right * walkingSpeed * thumbstickX * Time.deltaTime;
            }
        }
    }
}
