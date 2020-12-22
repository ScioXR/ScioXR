using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using WebXR;

public class WebXRInteractor : MonoBehaviour
{
    private FixedJoint attachJoint = null;
    private Rigidbody currentRigidBody = null;
    private List<Rigidbody> contactRigidBodies = new List<Rigidbody>();

    private Animator anim;
    public WebXRControllerDevice controller;

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
            Pickup();
        }

        if (controller.triggerPressed.wasReleasedThisFrame)
        {
            Drop();
        }

        // Use the controller button or axis position to manipulate the playback time for hand model.
        //anim.Play("Take", -1, normalizedTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.GetComponent<WebXRInteractable>())
            return;

        contactRigidBodies.Add(other.gameObject.GetComponent<Rigidbody>());
        //controller.Pulse(0.5f, 250);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.GetComponent<WebXRInteractable>())
            return;

        contactRigidBodies.Remove(other.gameObject.GetComponent<Rigidbody>());
    }

    public void Pickup()
    {
        currentRigidBody = GetNearestRigidBody();

        if (!currentRigidBody)
            return;

        currentRigidBody.MovePosition(transform.position);
        currentRigidBody.isKinematic = false;
        attachJoint.connectedBody = currentRigidBody;
    }

    public void Drop()
    {
        if (!currentRigidBody)
            return;

        attachJoint.connectedBody = null;
        currentRigidBody.isKinematic = true;
        currentRigidBody = null;
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
