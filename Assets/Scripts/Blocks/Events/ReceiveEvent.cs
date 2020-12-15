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
    }

    public override void Resolve(BlockData blockData)
    {
        messageName = blockData.paramString;
        MessagesManager.instance.AddMessageReceiver(this);
    }
}
