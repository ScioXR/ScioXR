using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnappingPanel : XRPanel
{
    Toggle enableSnap;
    InputField moveStep;
    InputField rotateStep;
    InputField scaleStep;

    public override void Show()
    {
        base.Show();

        enableSnap.isOn = EditorSettings.instance.enableSnap;
        moveStep.text = "" + EditorSettings.instance.snapMoveStep;
        rotateStep.text = "" + EditorSettings.instance.snapRotateStep;
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
        if (float.TryParse(rotateStep.text, out parseValue))
        {
            EditorSettings.instance.snapRotateStep = parseValue;
        }
        else
        {
            rotateStep.text = "" + EditorSettings.instance.snapRotateStep;
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
