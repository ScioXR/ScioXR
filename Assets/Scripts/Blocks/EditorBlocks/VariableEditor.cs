using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VariableEditor : BlockEditor
{
    public VariableAttachPoint attachPoint;
    public string variableName;

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);

        if (attachPoint != null)
        {
            attachPoint.GetComponent<BlockEditor>().DetachBlock(gameObject);
        }
    }

    public override bool IsAttached()
    {
        return attachPoint != null;
    }
}
