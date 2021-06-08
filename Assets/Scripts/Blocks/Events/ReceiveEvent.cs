using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveEvent : Event
{
    public string messageName;
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

        //Update is not called when the object is inactive and this is a workaround for objects that are hidden and that still need to receive the event
        if (!gameObject.activeSelf)
        {
            Poll();
        }
    }

    public override void Resolve(BlockData blockData)
    {
        messageName = blockData.paramString;
        MessagesManager.instance.AddMessageReceiver(this);
    }
}
