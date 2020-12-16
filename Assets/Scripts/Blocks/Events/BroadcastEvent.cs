using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadcastEvent : Block
{
    string messageName;

    //BoradcastEvent is not derrived from Events because it acts as a regular block
    public override void Do()
    {
        MessagesManager.instance.TriggerMessage(messageName);
        base.Do();
    }

    public override void Resolve(BlockData blockData)
    {
        messageName = blockData.paramString;
        base.Resolve(blockData);
    }
}
