using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPressedEvent : Event
{
    public KeyCode key;
    public override void Poll()
    {
        if (Input.GetKeyDown(key))
        {
            nextBlock.Do();
        }
    }
}
