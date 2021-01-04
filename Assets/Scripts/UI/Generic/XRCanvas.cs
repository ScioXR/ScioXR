using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine;
using UnityEngine.InputSystem;

public class XRCanvas : MonoBehaviour
{
    public GameObject UICanvas;
    public Transform handle;
    public XRPanel panel;
    public bool alwaysOn;
    
    public Vector3 offset;

    public bool isActive;
    private bool isGrabbed;

    private bool hasCamera;
    // Start is called before the first frame update
    void Start()
    {
        if (handle)
        {
            UICanvas.transform.SetParent(handle);
            handle.transform.localRotation = Quaternion.Euler(0, -180, 0);
        }

        if (!alwaysOn)
        {
            isActive = false;
            Hide();
        } else
        {
            GetComponentInChildren<Canvas>().worldCamera = CameraHelper.main;
        }
    }

    private void Update()
    {
        if (!hasCamera)
        {
            Camera camera = CameraHelper.main;
            if (camera)
            {
                GetComponentInChildren<Canvas>().worldCamera = camera;
                hasCamera = true;
            }
        }
    }

    public bool IsActive()
    {
        return isActive;
    }

    private void Show()
    {
        if (CameraHelper.main)
        {
            GetComponentInChildren<Canvas>().worldCamera = CameraHelper.main;

            this.gameObject.SetActive(true);

            SetPosition();
            panel.Show();
        }    
    }

    private void Hide()
    {
        this.gameObject.SetActive(false);
        panel.Hide();
        handle.transform.localPosition = new Vector3(0, 0, 0);
        handle.transform.localRotation = Quaternion.Euler(0, -180, 0);  
    }

    public void Toggle()
    {
        //Toggle is disabled if handle is grabbed
        if (!isGrabbed)
        {
            isActive = !isActive;
            if (isActive)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }
    }

    public void ToggleActionContext(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Toggle();
        }            
    }
   
    public void SetPosition()
    {
        GameObject cameraObject = CameraHelper.main.gameObject;
        Vector3 viewVector = new Vector3(cameraObject.transform.forward.x, 0, cameraObject.transform.forward.z).normalized;
        Vector3 newPosition = cameraObject.transform.position + viewVector * offset.z + Vector3.up * offset.y + cameraObject.transform.right * offset.x;
        transform.position = newPosition;
        transform.LookAt(new Vector3(cameraObject.transform.position.x, transform.position.y, cameraObject.transform.position.z));

    }

    public void OnSelectEnter()
    {
        isGrabbed = true;
    }

    public void OnSelectExit()
    {
        isGrabbed = false;
    }
 
}
