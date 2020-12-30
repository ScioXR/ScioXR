using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class WebXRUIInputModule : BaseInputModule
{
    public WebXRUIInteractor[] interactors;

    [SerializeField, FormerlySerializedAs("clickSpeed")]
    [Tooltip("The maximum time (in seconds) between two mouse presses for it to be consecutive click.")]
    float m_ClickSpeed = 0.3f;

    [FormerlySerializedAs("trackedDeviceDragThresholdMultiplier")]
    [SerializeField, Tooltip("Scales the EventSystem.pixelDragThreshold, for tracked devices, to make selection easier.")]
    float m_TrackedDeviceDragThresholdMultiplier = 2f;


    private List<RaycastResult> raycasts;
    protected override void Awake()
    {
        raycasts = new List<RaycastResult>();
    }

    public override void Process()
    {
        foreach (var interactor in interactors)
        {
            TrackedDeviceEventData eventData = interactor.GetTrackedDeviceData();
            eventData.position = new Vector2(float.MinValue, float.MinValue);

            raycasts.Clear();
            EventSystem.current.RaycastAll(eventData, raycasts);
            var result = FindFirstRaycast(raycasts);

            

            interactor.UpdateRay(result);

            eventData.pointerCurrentRaycast = result;

            Camera camera = CameraHelper.main;
            if (camera != null)
            {
                Vector2 screenPosition;
                if (eventData.pointerCurrentRaycast.isValid)
                {
                    screenPosition = camera.WorldToScreenPoint(eventData.pointerCurrentRaycast.worldPosition);
                }
                else
                {
                    var endPosition = eventData.rayPoints.Count > 0 ? eventData.rayPoints[eventData.rayPoints.Count - 1] : Vector3.zero;
                    screenPosition = camera.WorldToScreenPoint(endPosition);
                    eventData.position = screenPosition;
                }

                var thisFrameDelta = screenPosition - eventData.position;
                eventData.position = screenPosition;
                eventData.delta = thisFrameDelta;
            }
                

            ProcessButtons(interactor.GetButtonDeltaState(), eventData);
            ProcessMovement(eventData);
            ProcessDrag(eventData, m_TrackedDeviceDragThresholdMultiplier);

            
            //interactor.ProcessUI(result);
        }
    }

    private void ProcessMovement(PointerEventData eventData)
    {
        var currentPointerTarget = eventData.pointerCurrentRaycast.gameObject;

        // If we have no target or pointerEnter has been deleted,
        // we just send exit events to anything we are tracking
        // and then exit.
        if (currentPointerTarget == null || eventData.pointerEnter == null)
        {
            foreach (var go in eventData.hovered)
                ExecuteEvents.Execute(go, eventData, ExecuteEvents.pointerExitHandler);

            eventData.hovered.Clear();

            if (currentPointerTarget == null)
            {
                eventData.pointerEnter = null;
                return;
            }
        }

        if (eventData.pointerEnter == currentPointerTarget && currentPointerTarget)
            return;

        var commonRoot = FindCommonRoot(eventData.pointerEnter, currentPointerTarget);

        // We walk up the tree until a common root and the last entered and current entered object is found.
        // Then send exit and enter events up to, but not including, the common root.
        if (eventData.pointerEnter != null)
        {
            var t = eventData.pointerEnter.transform;

            while (t != null)
            {
                if (commonRoot != null && commonRoot.transform == t)
                    break;

                ExecuteEvents.Execute(t.gameObject, eventData, ExecuteEvents.pointerExitHandler);

                eventData.hovered.Remove(t.gameObject);

                t = t.parent;
            }
        }

        eventData.pointerEnter = currentPointerTarget;
        if (currentPointerTarget != null)
        {
            var t = currentPointerTarget.transform;

            while (t != null && t.gameObject != commonRoot)
            {
                ExecuteEvents.Execute(t.gameObject, eventData, ExecuteEvents.pointerEnterHandler);

                eventData.hovered.Add(t.gameObject);

                t = t.parent;
            }
        }
    }

    private void ProcessButtons(WebXRUIInteractor.ButtonDeltaState buttonChanges, PointerEventData eventData)
    {
        var currentOverGo = eventData.pointerCurrentRaycast.gameObject;

        if ((buttonChanges & WebXRUIInteractor.ButtonDeltaState.Pressed) != 0)
        {
            eventData.eligibleForClick = true;
            eventData.delta = Vector2.zero;
            eventData.dragging = false;
            eventData.pressPosition = eventData.position;
            eventData.pointerPressRaycast = eventData.pointerCurrentRaycast;

            var selectHandlerGO = ExecuteEvents.GetEventHandler<ISelectHandler>(currentOverGo);

            // If we have clicked something new, deselect the old thing
            // and leave 'selection handling' up to the press event.
            if (selectHandlerGO != eventSystem.currentSelectedGameObject)
                eventSystem.SetSelectedGameObject(null, eventData);

            // search for the control that will receive the press.
            // if we can't find a press handler set the press
            // handler to be what would receive a click.
            var newPressed = ExecuteEvents.ExecuteHierarchy(currentOverGo, eventData, ExecuteEvents.pointerDownHandler);

            // We didn't find a press handler, so we search for a click handler.
            if (newPressed == null)
                newPressed = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentOverGo);

            var time = Time.unscaledTime;

            if (newPressed == eventData.lastPress && ((time - eventData.clickTime) < m_ClickSpeed))
                ++eventData.clickCount;
            else
                eventData.clickCount = 1;

            eventData.clickTime = time;

            eventData.pointerPress = newPressed;
            eventData.rawPointerPress = currentOverGo;

            // Save the drag handler for drag events during this mouse down.
            eventData.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(currentOverGo);

            if (eventData.pointerDrag != null)
                ExecuteEvents.Execute(eventData.pointerDrag, eventData, ExecuteEvents.initializePotentialDrag);
        }

        if ((buttonChanges & WebXRUIInteractor.ButtonDeltaState.Released) != 0)
        {
            ExecuteEvents.Execute(eventData.pointerPress, eventData, ExecuteEvents.pointerUpHandler);

            var pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentOverGo);

            if (eventData.pointerPress == pointerUpHandler && eventData.eligibleForClick)
            {
                ExecuteEvents.Execute(eventData.pointerPress, eventData, ExecuteEvents.pointerClickHandler);
            }
            else if (eventData.dragging && eventData.pointerDrag != null)
            {
                ExecuteEvents.ExecuteHierarchy(currentOverGo, eventData, ExecuteEvents.dropHandler);
                ExecuteEvents.Execute(eventData.pointerDrag, eventData, ExecuteEvents.endDragHandler);
            }

            eventData.eligibleForClick = eventData.dragging = false;
            eventData.pointerPress = eventData.rawPointerPress = eventData.pointerDrag = null;
        }
    }

    private void ProcessDrag(PointerEventData eventData, float pixelDragThresholdMultiplier = 1.0f)
    {
        if (!eventData.IsPointerMoving() ||
                Cursor.lockState == CursorLockMode.Locked ||
                eventData.pointerDrag == null)
        {
            return;
        }

        if (!eventData.dragging)
        {
            if ((eventData.pressPosition - eventData.position).sqrMagnitude >= ((eventSystem.pixelDragThreshold * eventSystem.pixelDragThreshold) * pixelDragThresholdMultiplier))
            {
                ExecuteEvents.Execute(eventData.pointerDrag, eventData, ExecuteEvents.beginDragHandler);
                eventData.dragging = true;
            }
        }

        if (eventData.dragging)
        {
            // If we moved from our initial press object, process an up for that object.
            if (eventData.pointerPress != eventData.pointerDrag)
            {
                ExecuteEvents.Execute(eventData.pointerPress, eventData, ExecuteEvents.pointerUpHandler);

                eventData.eligibleForClick = false;
                eventData.pointerPress = null;
                eventData.rawPointerPress = null;
            }
            ExecuteEvents.Execute(eventData.pointerDrag, eventData, ExecuteEvents.dragHandler);
        }
    }
}
