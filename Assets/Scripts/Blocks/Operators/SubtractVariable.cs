using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtractVariable : Variable
{
    public Variable value1;
    public Variable value2;

    public override string GetText()
    {
        throw new System.NotImplementedException();
    }

    public override int GetValue()
    {
        return value1.GetValue() - value2.GetValue();
    }
}
