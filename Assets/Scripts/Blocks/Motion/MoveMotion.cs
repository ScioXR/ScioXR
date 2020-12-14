using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMotion : Block
{
    private int targetId;
    public GameObject targetObject;
    public Variable movingTime;

    private float timePassed;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool moving;

    public override void Do()
    {
        if (!targetObject)
        {
            targetObject = ScioXRSceneManager.instance.GetObject(targetId);
        }
        startPosition = codeController.gameObject.transform.position;
        endPosition = targetObject.transform.position;
        timePassed = 0;
        moving = true;
        //move current game object
        //codeController.gameObject.transform.position += movingTime.GetValue() * codeController.gameObject.transform.forward;
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
            codeController.gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, moveStep);
        }
    }

    public override void Resolve(BlockData blockData)
    {
        if (blockData.paramString != "")
        {
            //variable reference
        } else
        {
            //integer reference
            movingTime = new IntVariable(blockData.paramInt);
        }
        targetId = blockData.objectReference;
        //targetObject = targetId
    }
}
