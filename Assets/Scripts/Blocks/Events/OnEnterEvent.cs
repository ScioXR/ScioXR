using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnterEvent : Block
{
    public string validTag;

    public void OnTriggerEnter(Collider other)
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
