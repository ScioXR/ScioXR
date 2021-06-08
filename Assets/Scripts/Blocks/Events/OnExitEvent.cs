using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnExitEvent : Block
{
    public string validTag;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == validTag)
        {
            Do();
        }
    }
}
