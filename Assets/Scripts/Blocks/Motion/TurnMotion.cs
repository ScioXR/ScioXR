using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnMotion : Block
{
    public Variable rotateStep;

    public override void Do()
    {
        //move current game object
        codeController.gameObject.transform.Rotate(codeController.gameObject.transform.up, rotateStep.GetValue());
    }
}
