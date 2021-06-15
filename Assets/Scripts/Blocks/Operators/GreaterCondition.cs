using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreaterCondition : Condition
{
    public Variable value1;
    public Variable value2;

    public override bool IsTrue()
    {
        return value1.GetValue() > value2.GetValue();
    }

    public override void Resolve(BlockData blockData)
    {
        value1 = codeController.Resolve(blockData.attachedBlocks[0]) as Variable;
        value2 = codeController.Resolve(blockData.attachedBlocks[1]) as Variable;
    }
}
