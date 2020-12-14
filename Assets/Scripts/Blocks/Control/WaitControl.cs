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
        if (blockData.paramString != "")
        {

        } else
        {
            waitTime = new IntVariable(blockData.paramInt);
        }
    }

    public void DelayDo()
    {
        base.Do();
    }
}
