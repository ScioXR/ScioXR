using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractEvent : Event
{
    bool trigger;

    public override void Poll()
    {
        if (trigger)
        {
            trigger = false;
            base.Do();
        }
    }

    public void Trigger()
    {
        trigger = true;
    }

}
