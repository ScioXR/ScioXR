using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideLook : Block
{
    public override void Do()
    {
        gameObject.SetActive(false);
        base.Do();
    }
}
