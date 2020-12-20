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
    private List<RaycastResult> raycasts;

    private TrackedDeviceEventData eventData;

    private LineRenderer lineRenderer;
    private GameObject selectedObject;

    WebXRControllerDevice controllerDevice;

    // Start is called before the first frame update
    void Start()
    {
        eventData = new TrackedDeviceEventData(EventSystem.current);
        eventData.rayPoints = new List<Vector3>();
        eventData.rayPoints.Add(Vector3.zero);
        eventData.rayPoints.Add(Vector3.zero);

        raycasts = new List<RaycastResult>();
        lineRenderer = GetComponent<LineRenderer>();

        InternedString usage = GetComponent<WebXRController>().hand == WebXRControllerHand.LEFT ? CommonUsages.LeftHand : CommonUsages.RightHand;
        controllerDevice = InputSystem.GetDevice<WebXRControllerDevice>(usage);
    }

    // Update is called once per frame
    void Update()
    {
        eventData.layerMask = layerMask;
        eventData.rayPoints[0] = transform.position;
        eventData.rayPoints[1] = (transform.position + transform.forward * 100);
        raycasts.Clear();
        EventSystem.current.RaycastAll(eventData, raycasts);
        lineRenderer.enabled = raycasts.Count > 0;
        if (raycasts.Count > 0)
        {
            if (selectedObject != null)
            {
                bool isSame = raycasts.Any(x => x.gameObject == selectedObject);
                if (!isSame)
                {
                    ExecuteEvents.Execute(selectedObject, eventData, ExecuteEvents.pointerExitHandler);
                    selectedObject = null;
                }
            }

            if (selectedObject == null)
            {
                foreach (var raycast in raycasts)
                {
                    bool success = ExecuteEvents.Execute(raycast.gameObject, eventData, ExecuteEvents.pointerEnterHandler);
                    if (success)
                    {
                        selectedObject = raycast.gameObject;
                        break;
                    }
                }
            }

            if (selectedObject != null)
            {
                if (controllerDevice.primaryButton.wasPressedThisFrame)
                {
                    bool success = ExecuteEvents.Execute(selectedObject, eventData, ExecuteEvents.pointerClickHandler);
                }
            }

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, raycasts[0].worldPosition);
        }
        else
        {
            if (selectedObject != null)
            {
                ExecuteEvents.Execute(selectedObject, eventData, ExecuteEvents.pointerExitHandler);
                selectedObject = null;
            }
        }
    }
}
