using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyControl : Block
{
    public override void Do()
    {
        Destroy(gameObject);
    }
}
