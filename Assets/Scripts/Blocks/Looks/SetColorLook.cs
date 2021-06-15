using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColorLook : Block
{
    Color color;

    public override void Do()
    {
        GetComponent<MeshRenderer>().material.color = color;
        base.Do();
    }

    public override void Resolve(BlockData blockData)
    {
        Color serializedColor;
        ColorUtility.TryParseHtmlString("#" + blockData.paramString, out serializedColor);
        color = serializedColor;
    }
}
