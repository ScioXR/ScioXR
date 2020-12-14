using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class EditorTransformXR : MonoBehaviour
{

    private Vector3 initialAttachLocalPos;
    private Quaternion initialAttachLocalRot;
    private InputDevice controller;
    private List<InputDevice> devices = new List<InputDevice>();
    private XRNode controllerNode = XRNode.RightHand;
    public float speed = 10.0f;
    public float threshold = 0.1f;
    public bool isEditMode;
    public bool isChange = false;
    private GameObject rayObject;
    private XRGrabInteractable grab;
    private bool isRightHandInCollision;
    private bool isLeftHandInCollision;
    private float scale;
    public float value;
    private bool isHover;
    public static bool isSelected;
    private Quaternion currentRotation;

    public GameObject grabPoint;
    public Vector3 currentPosition;
    public Vector3 newPosition;

    public static Material originalMaterial;

    void Start()
    {
        GetDevice();
        grab = GetComponent<XRGrabInteractable>();
        grab.onSelectEntered.AddListener(OnSelectEnter);
        grab.onSelectExited.AddListener(OnSelectExit);
        scale = transform.localScale.x;
        originalMaterial = GetComponent<MeshRenderer>().material;
        if (!grab.attachTransform)
        {
            grabPoint = new GameObject("Grab Pivot");
            grabPoint.transform.SetParent(transform, false);
            grab.attachTransform = grabPoint.transform;
        }
         initialAttachLocalPos = grab.attachTransform.localPosition;
         initialAttachLocalRot = grab.attachTransform.localRotation;        
    }
    private void Update()
    {
        if (devices == null)
        {
            GetDevice();
        }
        value = LocomotionController.distance;
        scale = transform.localScale.x;
        //selected
        /* if (XRInputManager.EnableRight && isHover)
         {
             //XRCanvas.instance.Toggle(); 
             EventManager.TriggerEvent("Toggle");
             isSelected = true;
             selectedObject = this.gameObject;
         }*/
       // SelectedObject();

        if (isEditMode)
        {
            Vector3 forward = rayObject.transform.TransformDirection(Vector3.forward);
            UpdateMovement(forward);
        }
        if (EditorManager.mode == TransformMode.SCALE)
        {
            if (isRightHandInCollision && isLeftHandInCollision)
            {
                if (scale != value)
                {
                    scale = value;
                    transform.localScale = new Vector3(scale, scale, scale);
                }
            }
        }
        /* if (isRightHandInCollision && isLeftHandInCollision && isEditMode)
         {
             if (scale != value)
             {
                 scale = value;
                 transform.localScale = new Vector3(scale, scale, scale);
             }
         }*/
    }
    private void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(controllerNode, devices);
        controller = devices.FirstOrDefault();
    }
    public void OnSelectEnter(XRBaseInteractor interactor)
    {   
        if (interactor is XRRayInteractor || interactor is XRDirectInteractor)
        {
            
            if (EditorManager.mode == TransformMode.PROPERTIES)
            {
                //open properties menu
                EditorManager.instance.ToggleProperties(gameObject);
               
            }
            else if (EditorManager.mode == TransformMode.CLONE)
            {
                EditorManager.instance.DuplicateObject(gameObject);
            }
            grab.attachTransform.position = interactor.transform.position;
            grab.attachTransform.rotation = interactor.transform.rotation;
            rayObject = interactor.gameObject;
            isEditMode = true;

        }    
        else
        {
            grab.attachTransform.localPosition = initialAttachLocalPos;
            grab.attachTransform.localRotation = initialAttachLocalRot;
        }
    }

    public void OnSelectExit(XRBaseInteractor interactor)
    {
        isEditMode = false;
    }

    private void UpdateMovement(Vector3 forward)
    {
        Vector2 primary2dValue;

        InputFeatureUsage<Vector2> primary2DVector = CommonUsages.primary2DAxis;

        if (controller.TryGetFeatureValue(primary2DVector, out primary2dValue) && primary2dValue != Vector2.zero)
        {
            float xAxis = primary2dValue.x * speed * Time.deltaTime;
            float zAxis = primary2dValue.y * speed * Time.deltaTime;
            UpdateRotation(xAxis);
            UpdatePosition(zAxis, forward);

        }
    }
    Vector3 fromOriginToObject;
    public void UpdatePosition(float zAxis, Vector3 forward)
    {

       // newPosition = this.transform.position;
        if (zAxis >= threshold || zAxis < -threshold)
        {
            // transform.position += forward * zAxis;

            //Debug.Log("Z  " + zAxis);
           // newPosition = this.transform.position;
            isChange = true;
            newPosition = this.transform.position + forward * zAxis;
             if (newPosition.z < 6 && newPosition.z > 0.7f)
             {  
                 this.transform.position = newPosition;
                 Debug.Log("NewPosition  " + newPosition.z);
             }

        }
        else
        {
            this.transform.position = newPosition;
            //grab.attachTransform.position = newPosition;
            //grabPoint.transform.localPosition = newPosition;
            //Debug.Log("ElsePosition  " + grabPoint.transform.localPosition);
        }
       
    }
    public void UpdateRotation(float xAxis)
    {
        if (currentRotation != this.transform.rotation)
        {
            currentRotation = this.transform.rotation;
        }
        if (xAxis >= threshold || xAxis < -threshold)
        {
            transform.Rotate(0, 0, xAxis * speed);
           // Debug.Log("xAxis " + primary2dValue.x);
            isChange = true;
            currentRotation = this.transform.rotation;
        }
        else
        {
            this.transform.rotation = currentRotation;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RightHand")
        {
            isRightHandInCollision = true;
        }
        if (other.gameObject.tag == "LeftHand")
        {
            isLeftHandInCollision = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "RightHand")
        {
            isRightHandInCollision = false;
        }
        if (other.gameObject.tag == "LeftHand")
        {
            isLeftHandInCollision = false;
        }
    }    
}
