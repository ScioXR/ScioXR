using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsTowardMotion : Block
{
    private int targetId;
    public GameObject targetObject;
    public Variable movingTime;

    private float timePassed;
    private Quaternion startRotation;
    private Quaternion endRotation;
    private bool moving;

    public override void Do()
    {
        if (!targetObject)
        {
            targetObject = ScioXRSceneManager.instance.GetObject(targetId);
        }

        startRotation = codeController.gameObject.transform.rotation;
        codeController.gameObject.transform.LookAt(targetObject.transform.position);
        endRotation = codeController.gameObject.transform.rotation;

        codeController.gameObject.transform.rotation = startRotation;
        timePassed = 0;
        moving = true;
    }

    private void Update()
    {
        if (moving)
        {
            timePassed += Time.deltaTime;
            float moveStep = timePassed / movingTime.GetValue();
            if (moveStep > 1)
            {
                moveStep = 1;
                moving = false;
                base.Do();
            }
            codeController.gameObject.transform.rotation = Quaternion.Slerp(startRotation, endRotation, moveStep);
        }
    }

    public override void Resolve(BlockData blockData)
    {
        movingTime = codeController.Resolve(blockData.attachedBlocks[0]) as Variable;
        targetId = blockData.objectReference;
    }
}
