using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnterEvent : Block
{
    public string validTag;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == validTag)
        {
            Do();
        }
    }
}
