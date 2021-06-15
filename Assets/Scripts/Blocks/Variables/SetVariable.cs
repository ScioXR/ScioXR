using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVariable : Block
{
    public IntVariable intVariable;
    public Variable newValue;

    public override void Do()
    {
        intVariable.value = newValue.GetValue();
        base.Do();
    }

    public override void Resolve(BlockData blockData)
    {
        intVariable = VariablesManager.instance.GetVariable(blockData.paramString);
        newValue = codeController.Resolve(blockData.attachedBlocks[0]) as Variable;
    }
}
