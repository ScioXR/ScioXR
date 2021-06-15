using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTextLook : Block
{
    Variable text;

    public override void Do()
    {
        if (GetComponent<Saveable>())
        {
            GetComponent<Saveable>().data.text.text = text.GetText();
            GetComponent<Saveable>().UpdateText();
        }
        base.Do();
    }

    public override void Resolve(BlockData blockData)
    {
        Block variableBlock = codeController.Resolve(blockData.attachedBlocks[0]);
        text = variableBlock as Variable;
    }
}
