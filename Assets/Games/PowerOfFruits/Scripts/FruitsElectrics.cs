using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class FruitsElectrics : MonoBehaviour
{

    public bool isConnected;

    // public bool isConnected;
    public GameObject[] connectedObject;

    public float electricity;

    private void Start()
    {
        connectedObject = new GameObject[2];
    }


    private void Update()
    {
    }

    public void OnSnap(object gameObject, SnapDropZoneEventArgs snap)
    {
        if (snap.snappedObject.transform.parent.GetComponent<ControllerWire>())
        {
            snap.snappedObject.transform.parent.GetComponent<ControllerWire>().Connect(this);
            Connect(snap.snappedObject);
            BulbController.instance.CalculateElectrics();
        }
    }

    public void OnUnSnap(object gameObject, SnapDropZoneEventArgs snap)
    {
        Debug.Log("OnUnSnap: " + snap.snappedObject);
        if (snap.snappedObject.transform.parent.GetComponent<ControllerWire>())
        {
            snap.snappedObject.transform.parent.GetComponent<ControllerWire>().Dissconnect(this);
            Dissconnect(snap.snappedObject);
            BulbController.instance.CalculateElectrics();
        }
    }

    public void Connect(GameObject connected)
    {
        for (int i = 0; i < connectedObject.Length; i++)
        {
            if (connectedObject[i] == null)
            {
                connectedObject[i] = connected;
                break;
            }
        }
    }

    public void Dissconnect(GameObject connected)
    {
        for (int i = 0; i < connectedObject.Length; i++)
        {
            if (connectedObject[i] == connected)
            {
                connectedObject[i] = null;
                break;
            }
        }
    }

    public bool IsGoodConnection()
    {
        return connectedObject[0] && connectedObject[1] && connectedObject[0].tag != connectedObject[1].tag;
    }

    public static int steps = 0;

    public virtual float GetElectricity(ControllerWire wire)
    {
       // Debug.Log("GetElectricity: " + gameObject.name + ", " + wire.name);
        float e = -1;
        if (IsGoodConnection())
        {
           // Debug.Log("Other wire to: " + GetOtherWire(wire).name);
            if (GetOtherWire(wire).GetOther(this))
            {
               // Debug.Log("Other fruit: " + GetOtherWire(wire).GetOther(this).name);
                float otherElectricity = GetOtherWire(wire).GetOther(this).GetElectricity(GetOtherWire(wire));
                if (otherElectricity >= 0)
                {
                    e = otherElectricity + electricity;
                }
            }
        }
        //Debug.Log("GetElectricity return: " + gameObject.name + ", " + e);
        return e;
    }

    public ControllerWire GetOtherWire(ControllerWire wire)
    {
        GameObject connected = connectedObject[0].transform.parent.gameObject == wire.gameObject ? connectedObject[1] : connectedObject[0];
        return connected.transform.parent.GetComponent<ControllerWire>();
    }
}
