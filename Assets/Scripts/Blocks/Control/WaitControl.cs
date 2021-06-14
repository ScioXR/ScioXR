using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitControl : Block
{
    Variable waitTime;

    public override void Do()
    {
        Invoke("DelayDo", waitTime.GetValue());
    }

    public override void Resolve(BlockData blockData)
    {
        Block variableBlock = codeController.Resolve(blockData.attachedBlocks[0]);
        waitTime = variableBlock as Variable;
    }

    public void DelayDo()
    {
        base.Do();
    }
}
