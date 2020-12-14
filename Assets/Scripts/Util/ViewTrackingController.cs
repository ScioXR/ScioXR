using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewTrackingController : MonoBehaviour
{
    public float trackSpeed;
    public float viewAngle;
    public float deadZone;
    private GameObject trackedObject;

    private Vector3 targetPosition;
    private Vector3 trackedPosition;
    bool moving;

    public float distanceToTarget;
    public bool calculateDistance = true;

    // Start is called before the first frame update
    void Start()
    {
        trackedObject = GameObject.FindGameObjectWithTag("MainCamera");
        if (trackedObject != null)
        {
            transform.LookAt(new Vector3(trackedObject.transform.position.x, transform.position.y, trackedObject.transform.position.z));
            SetupTracking();
        }
       
    }

    private void SetupTracking()
    {
        if (calculateDistance)
        {
            distanceToTarget = (transform.position - new Vector3(trackedObject.transform.position.x, transform.position.y, trackedObject.transform.position.z)).magnitude;

            transform.LookAt(new Vector3(trackedObject.transform.position.x, transform.position.y, trackedObject.transform.position.z));
        }
        else
        {
           // distanceToTarget =AlterEyes.DojoAPI.GameConfig.Settings.tutorialTrackingDistance;
            //set to predefined distance in the field of view
            trackedPosition = new Vector3(trackedObject.transform.position.x, transform.position.y, trackedObject.transform.position.z);
            Vector3 directionVector = transform.position - trackedPosition;
            transform.position = trackedPosition + directionVector.normalized * distanceToTarget;
        }
       // viewAngle =AlterEyes.DojoAPI.GameConfig.Settings.tutorialTrackingAngle;
        //trackSpeed =AlterEyes.DojoAPI.GameConfig.Settings.tutorialTrackingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (trackedObject != null)
        {
            //calculate the angle between the tracked object and this object
            Vector3 panelVector = (new Vector3(transform.position.x, trackedObject.transform.position.y, transform.position.z) - trackedObject.transform.position);
            Vector3 viewVector = new Vector3(trackedObject.transform.forward.x, 0, trackedObject.transform.forward.z).normalized;

            //Debug.Log(panelVector + ", " + viewVector);

            float angle = Vector3.Angle(panelVector, viewVector);
            //Debug.Log(angle);

            if (angle > viewAngle && !moving)
            {
                StartMoving();
            }

            if (moving)
            {
                //   Debug.Log((targetPosition - transform.position).magnitude);
                CalculateTargetPosition();

                trackedPosition = new Vector3(trackedObject.transform.position.x, transform.position.y, trackedObject.transform.position.z);

                //transform.position += (targetPosition - transform.position).normalized * trackSpeed * Time.deltaTime;
                //transform.position = Vector3.Lerp(transform.position, targetPosition, trackSpeed * Time.deltaTime);

                transform.position = Vector3.Slerp(transform.position - trackedPosition, targetPosition - trackedPosition, trackSpeed * Time.deltaTime);
                transform.position += trackedPosition;

                //fix target distance
                Vector3 fixedTargetPosition = trackedPosition + (targetPosition - trackedPosition).normalized * distanceToTarget;
                targetPosition = fixedTargetPosition;

                if ((targetPosition - transform.position).magnitude < deadZone)
                {
                    //transform.position = targetPosition;
                    moving = false;
                }



                transform.LookAt(new Vector3(trackedObject.transform.position.x, transform.position.y, trackedObject.transform.position.z));
            }

            Debug.DrawLine(trackedObject.transform.position, trackedObject.transform.position + panelVector * 5, Color.green);
            Debug.DrawLine(trackedObject.transform.position, trackedObject.transform.position + viewVector * 5, Color.red);
        }
        else
        {
            trackedObject = GameObject.FindGameObjectWithTag("MainCamera");
            SetupTracking();
        }

    }

    void StartMoving()
    {
        moving = true;
        CalculateTargetPosition();

        trackedPosition = new Vector3(trackedObject.transform.position.x, transform.position.y, trackedObject.transform.position.z);
    }

    void CalculateTargetPosition()
    {
        //Vector3 panelVector = (new Vector3(transform.position.x, trackedObject.transform.position.y, transform.position.z) - trackedObject.transform.position);
        Vector3 viewVector = new Vector3(trackedObject.transform.forward.x, 0, trackedObject.transform.forward.z).normalized;

        Vector3 newPosition = trackedObject.transform.position + viewVector * distanceToTarget;
        targetPosition = new Vector3(newPosition.x, transform.position.y, newPosition.z);
    }
  
}
