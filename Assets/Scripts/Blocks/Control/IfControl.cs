using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfControl : Block
{
    public Condition condition;
    public Block insideBlock;

    public override void Do()
    {
        if (condition.IsTrue())
        {
            if (insideBlock)
            {
                insideBlock.Do();
            }
        }
        base.Do();
    }
}
