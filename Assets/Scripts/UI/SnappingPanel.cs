using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnappingPanel : XRPanel
{
    public Toggle enableSnap;
    public InputField moveStep;
    public InputField rotateStepY;
    public InputField rotateStepX;
    public InputField rotateStepZ;
    public InputField scaleStep;

    public override void Show()
    {
        base.Show();

        enableSnap.isOn = EditorSettings.instance.enableSnap;
        moveStep.text = "" + EditorSettings.instance.snapMoveStep;
        rotateStepY.text = "" + EditorSettings.instance.snapRotateStepY;
        rotateStepX.text = "" + EditorSettings.instance.snapRotateStepX;
        rotateStepZ.text = "" + EditorSettings.instance.snapRotateStepZ;
        scaleStep.text = "" + EditorSettings.instance.snapScaleStep;
    }

    public void ApplyData()
    {
        EditorSettings.instance.enableSnap = enableSnap.isOn;
        float parseValue;
        if (float.TryParse(moveStep.text, out parseValue)) {
            EditorSettings.instance.snapMoveStep = parseValue;
        } else
        {
            moveStep.text = "" + EditorSettings.instance.snapMoveStep;
        }
        if (float.TryParse(rotateStepY.text, out parseValue))
        {
            EditorSettings.instance.snapRotateStepY = parseValue;
        }
        else
        {
            rotateStepY.text = "" + EditorSettings.instance.snapRotateStepY;
        }
        if (float.TryParse(rotateStepX.text, out parseValue))
        {
            EditorSettings.instance.snapRotateStepX = parseValue;
        }
        else
        {
            rotateStepX.text = "" + EditorSettings.instance.snapRotateStepX;
        }
        if (float.TryParse(rotateStepZ.text, out parseValue))
        {
            EditorSettings.instance.snapRotateStepZ = parseValue;
        }
        else
        {
            rotateStepZ.text = "" + EditorSettings.instance.snapRotateStepZ;
        }
        if (float.TryParse(scaleStep.text, out parseValue))
        {
            EditorSettings.instance.snapScaleStep = parseValue;
        }
        else
        {
            scaleStep.text = "" + EditorSettings.instance.snapScaleStep;
        }
    }
}
