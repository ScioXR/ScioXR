using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLook : Block
{
    public override void Do()
    {
        gameObject.SetActive(true);
        base.Do();
    }
}
