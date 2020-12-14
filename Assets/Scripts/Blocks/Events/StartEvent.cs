using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEvent : Event
{
    bool started = false;

    public override void Poll()
    {
        if (!started)
        {
            started = true;
            nextBlock.Do();
        }
    }
}
