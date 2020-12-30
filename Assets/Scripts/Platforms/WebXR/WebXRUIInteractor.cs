using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.XR.Interaction.Toolkit.UI;
using WebXR;

public class WebXRUIInteractor : MonoBehaviour
{
    public LayerMask layerMask;

    private TrackedDeviceEventData eventData;

    private LineRenderer lineRenderer;

    WebXRControllerDevice controllerDevice;

    public enum ButtonDeltaState
    {
        NoChange = 0,
        Pressed = 1,
        Released = 2
    }

    private ButtonDeltaState buttonDeltaState;

    // Start is called before the first frame update
    void Start()
    {
        eventData = new TrackedDeviceEventData(EventSystem.current);
        eventData.rayPoints = new List<Vector3>();
        eventData.rayPoints.Add(Vector3.zero);
        eventData.rayPoints.Add(Vector3.zero);

        lineRenderer = GetComponent<LineRenderer>();

        InternedString usage = GetComponent<WebXRController>().hand == WebXRControllerHand.LEFT ? CommonUsages.LeftHand : CommonUsages.RightHand;
        controllerDevice = InputSystem.GetDevice<WebXRControllerDevice>(usage);
    }

    private GameObject draggingObject;
    private Vector3 dragStart;

    public TrackedDeviceEventData GetTrackedDeviceData()
    {
        eventData.layerMask = layerMask;
        eventData.rayPoints[0] = transform.position;
        eventData.rayPoints[1] = (transform.position + transform.forward * 100);
        return eventData;
    }

    public ButtonDeltaState GetButtonDeltaState()
    {
        buttonDeltaState = ButtonDeltaState.NoChange;
        if (controllerDevice.triggerPressed.wasPressedThisFrame)
        {
            buttonDeltaState |= ButtonDeltaState.Pressed;
        }
        if (controllerDevice.triggerPressed.wasReleasedThisFrame)
        {
            buttonDeltaState |= ButtonDeltaState.Released;
        }
        return buttonDeltaState;
    }

    public void UpdateRay(RaycastResult hitResult)
    {
        lineRenderer.enabled = hitResult.isValid;
        if (hitResult.isValid)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hitResult.worldPosition);
        }
    }
}
