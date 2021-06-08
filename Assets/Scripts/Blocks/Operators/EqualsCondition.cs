using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EqualsCondition : Condition
{
    public Variable value1;
    public Variable value2;

    public override bool IsTrue()
    {
        return value1.GetValue() == value2.GetValue();
    }
}
