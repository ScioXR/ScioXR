using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPlace : MonoBehaviour
{
    public GameObject snappedObject;

    public bool CanSnap()
    {
        return snappedObject == null;
    }
}
