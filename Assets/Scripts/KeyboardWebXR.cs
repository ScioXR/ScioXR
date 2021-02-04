using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebXR;

public class KeyboardWebXR : VRKeys.Keyboard
{
    private WebXRController leftHandDevice;
    private WebXRController rightHandDevice;

    public InputField textInput;

    private void OnEnable()
    {
        OnUpdate.AddListener(HandleUpdate);
        OnSubmit.AddListener(HandleSubmit);
        OnCancel.AddListener(HandleCancel);
    }

    private void OnDisable()
    {
        OnUpdate.RemoveListener(HandleUpdate);
        OnSubmit.RemoveListener(HandleSubmit);
        OnCancel.RemoveListener(HandleCancel);
    }

    public void HandleUpdate(string text)
    {
        textInput.text = text;
    }

    public void HandleSubmit(string text)
    {
        textInput.DeactivateInputField();
        Disable();
    }

    public void HandleCancel()
    {
        textInput.DeactivateInputField();
        Debug.Log("Cancelled keyboard input!");
    }

    // Update is called once per frame
    void Update()
    {
        if (leftHandDevice == null || rightHandDevice == null)
        {
            GetHands();
        }

        if (leftHandDevice)
        {
            leftHand.transform.localPosition = leftHandDevice.transform.position;
            leftHand.transform.localRotation = leftHandDevice.transform.rotation;
        }

        if (rightHandDevice)
        {
            rightHand.transform.localPosition = rightHandDevice.transform.position;
            rightHand.transform.localRotation = rightHandDevice.transform.rotation;
        }
    }

    private void GetHands()
    {
        WebXRController[] controllers = FindObjectsOfType<WebXRController>();
        foreach (var controller in controllers)
        {
            if (controller.hand == WebXRControllerHand.LEFT)
            {
                leftHandDevice = controller;
            }
            else if (controller.hand == WebXRControllerHand.RIGHT)
            {
                rightHandDevice = controller;
            }
        }
    }

    public void UpdatePosition()
    {
        GameObject cameraObject = CameraHelper.main.gameObject;
        Vector3 viewVector = new Vector3(cameraObject.transform.forward.x, 0, cameraObject.transform.forward.z).normalized;
        Vector3 newPosition = cameraObject.transform.position + viewVector * positionRelativeToUser.z + Vector3.up * positionRelativeToUser.y + cameraObject.transform.right * positionRelativeToUser.x;
        transform.position = newPosition;
        transform.rotation = Quaternion.LookRotation(transform.position - new Vector3(cameraObject.transform.position.x, transform.position.y, cameraObject.transform.position.z));
        //transform.LookAt(new Vector3(cameraObject.transform.position.x, transform.position.y, cameraObject.transform.position.z));

    }
}
