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

    public override void Resolve(BlockData blockData)
    {
        condition = codeController.Resolve(blockData.attachedBlocks[0]) as Condition;

        Block lastBlock = null;
        foreach (var childBlockData in blockData.childBlocks)
        {
            Block childBlock = codeController.Resolve(childBlockData);
            if (!lastBlock)
            {
                insideBlock = childBlock;
            } else
            {
                lastBlock.AddBlock(childBlock);
            }
            lastBlock = childBlock;
        }
    }
}
