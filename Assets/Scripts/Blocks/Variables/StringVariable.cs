using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringVariable : Variable
{
    public string value;

    public override int GetValue()
    {
        int intValue;
        if (!int.TryParse(value, out intValue))
        {
            intValue = 0;
        }
        return intValue;
    }

    public override string GetText()
    {
        return "" + value;
    }

    public override void Resolve(BlockData blockData)
    {
        value = blockData.paramString;
    }
}
