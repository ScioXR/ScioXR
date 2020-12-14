using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntVariable : Variable
{
    public int value;

    public IntVariable(int initValue)
    {
        value = initValue;
    }

    public override int GetValue()
    {
        return value;
    }

    public override string GetText()
    {
        return "" + value;
    }
}
