using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnExitEvent : Block
{
    public string validTag;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Saveable>() && other.gameObject.GetComponent<Saveable>().data.tag == validTag)
        {
            Do();
        }
    }

    public override void Resolve(BlockData blockData)
    {
        validTag = blockData.paramString;
    }
}
