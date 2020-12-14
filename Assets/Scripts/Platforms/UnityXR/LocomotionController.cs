using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionController : MonoBehaviour
{
    public static LocomotionController instance;
    public XRController leftTeleportRay;
    public XRController rightTeleportRay;
    public GameObject leftHand;
    public GameObject rightHand;
    public InputHelpers.Button teleportActivationButton;
    public float activationThreshold = 0.1f;

    public XRRayInteractor leftInteractionRay;
    public XRRayInteractor rightInteractionRay;

    public bool EnableLeftTeleport { get; set; } = true;
    public bool EnableRightTeleport { get; set; } = true;

    public static float distance;

    // Update is called once per frame
    private void Start()
    {
        instance = this;
    }
    void Update()
    {
        Vector3 position = new Vector3();
        Vector3 norm = new Vector3();
        int index = 0;
        bool validTarget = false;

        if (leftTeleportRay)
        {
            bool isLeftInteractionRayHovering = leftInteractionRay.TryGetHitInfo(ref position, ref norm, ref index, ref validTarget);
            leftTeleportRay.gameObject.SetActive(EnableLeftTeleport && CheckIfActivated(leftTeleportRay) && !leftInteractionRay);
        }

        if (rightTeleportRay)
        {
            bool isRightInteractionRayHovering = rightInteractionRay.TryGetHitInfo(ref position, ref norm, ref index, ref validTarget);
            rightTeleportRay.gameObject.SetActive(EnableRightTeleport && CheckIfActivated(rightTeleportRay) && !rightInteractionRay);
        }
        distance = Vector3.Distance(rightHand.transform.position, leftHand.transform.position);
        //Debug.Log("Distance  " + distance);
    }


    public bool CheckIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, activationThreshold);
        return isActivated;
    }
}