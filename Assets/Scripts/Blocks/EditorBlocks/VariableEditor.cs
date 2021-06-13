using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class VariableEditor : BlockEditor
{
    public string variableName;

    public override bool IsAttached()
    {
        return attachPoint != null;
    }

    public override void ExportData(ref BlockData blockData)
    {
        base.ExportData(ref blockData);
        blockData.paramString = variableName;
    }

    public override void ImportData(BlockData blockData)
    {
        base.ImportData(blockData);
        variableName = blockData.paramString;
        GetComponentInChildren<TextMeshProUGUI>().text = variableName;
    }
}
