using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Variable : Block
{
    public abstract int GetValue();

    public abstract string GetText();
}
