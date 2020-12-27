using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using WebXR;
using static UnityEngine.InputSystem.InputAction;

public class WebXRInteractor : MonoBehaviour
{
    public FixedJoint attachJoint = null;
    private List<Rigidbody> contactRigidBodies = new List<Rigidbody>();

    private Animator anim;
    public WebXRControllerDevice controller;

    private Rigidbody highlightedRigidbody;

    void Awake()
    {
        attachJoint = GetComponent<FixedJoint>();
    }

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        InternedString usage = GetComponent<WebXRController>().hand == WebXRControllerHand.LEFT ? CommonUsages.LeftHand : CommonUsages.RightHand;
        controller = InputSystem.GetDevice<WebXRControllerDevice>(usage);
    }

    void Update()
    {
        if (controller.triggerPressed.wasPressedThisFrame)
        {
            if (highlightedRigidbody)
            {
                IWebXRInteractable[] interactables = highlightedRigidbody.GetComponents<IWebXRInteractable>();
                foreach (var interactable in interactables)
                {
                    interactable.OnGrab(this);
                }
            }
            //Pickup();
        }

        if (controller.triggerPressed.wasReleasedThisFrame)
        {
            //Drop();
            if (highlightedRigidbody)
            {
                IWebXRInteractable[] interactables = highlightedRigidbody.GetComponents<IWebXRInteractable>();
                foreach (var interactable in interactables)
                {
                    interactable.OnUngrab(this);
                }
            }
        }

        if (controller.primaryButton.wasPressedThisFrame)
        {
            if (highlightedRigidbody)
            {
                IWebXRInteractable[] interactables = highlightedRigidbody.GetComponents<IWebXRInteractable>();
                foreach (var interactable in interactables)
                {
                    interactable.OnSecondaryGrab(this);
                }
            }
        }

        if (controller.primaryButton.wasReleasedThisFrame)
        {
            if (highlightedRigidbody)
            {
                IWebXRInteractable[] interactables = highlightedRigidbody.GetComponents<IWebXRInteractable>();
                foreach (var interactable in interactables)
                {
                    interactable.OnSecondaryUngrab(this);
                }
            }
        }

        // Use the controller button or axis position to manipulate the playback time for hand model.
        anim.Play("Take", -1, controller.trigger.ReadValue());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IWebXRInteractable>() == null)
            return;

        contactRigidBodies.Add(other.gameObject.GetComponent<Rigidbody>());

        UpdateHighlights();
        //controller.Pulse(0.5f, 250);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<IWebXRInteractable>() == null)
            return;

        contactRigidBodies.Remove(other.gameObject.GetComponent<Rigidbody>());

        UpdateHighlights();
    }

    private void UpdateHighlights()
    {
        Rigidbody nearestRigidbody = GetNearestRigidBody();
        if (nearestRigidbody != highlightedRigidbody)
        {
            if (highlightedRigidbody)
            {
                highlightedRigidbody.gameObject.GetComponent<Outline>().enabled = false;
            }

            if (nearestRigidbody)
            {
                nearestRigidbody.gameObject.GetComponent<Outline>().enabled = true;
            }
            highlightedRigidbody = nearestRigidbody;
        }
    }

    private Rigidbody GetNearestRigidBody()
    {
        Rigidbody nearestRigidBody = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach (Rigidbody contactBody in contactRigidBodies)
        {
            distance = (contactBody.gameObject.transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                nearestRigidBody = contactBody;
            }
        }

        return nearestRigidBody;
    }
}
