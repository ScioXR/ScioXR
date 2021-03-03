using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using WebXR;

public class WebXRMoveController : MonoBehaviour
{
    public float walkingSpeed = 1.0f;

    public enum ForwardMove
    {
        NONE,
        WALK,
    }

    public enum HorizontalMove
    {
        NONE,
        STRAFE,
        ROTATE
    }

    public ForwardMove forwardMoveType;
    public HorizontalMove horizontalMoveType;

    public float rotateSpeed;

    WebXRControllerDevice controllerDevice;

    public WebXRControllerHand hand;

    private bool waitRelease;

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
            float thumbstickX = controllerDevice.thumbstick.x.ReadValue();
            float thumbstickY = controllerDevice.thumbstick.y.ReadValue();

            if (forwardMoveType == ForwardMove.WALK)
            {
                Vector3 forward = CameraHelper.main.transform.TransformDirection(Vector3.forward);
                forward.y = 0;
                Vector3 right = CameraHelper.main.transform.TransformDirection(Vector3.right);
                right.y = 0;

                if (thumbstickY != 0)
                {
                    transform.position += forward * walkingSpeed * thumbstickY * Time.deltaTime;
                }
                if (thumbstickX != 0)
                {
                    if (horizontalMoveType == HorizontalMove.STRAFE)
                    {
                        transform.position += right * walkingSpeed * thumbstickX * Time.deltaTime;
                    }
                }
            }

            if (horizontalMoveType == HorizontalMove.ROTATE)
            {
                bool pressed = controllerDevice.thumbstick.x.IsPressed(0.5f);
                if (!waitRelease && pressed)
                {
                    transform.Rotate(Vector3.up, rotateSpeed * (thumbstickX > 0 ? 1 : -1));
                    waitRelease = true;
                } else if (waitRelease && !pressed)
                {
                    waitRelease = false;
                }
            }
        }
    }
}
