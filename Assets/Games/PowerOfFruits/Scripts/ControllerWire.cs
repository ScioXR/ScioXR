using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerWire : MonoBehaviour
{
    public FruitsElectrics[] connectedObject;

    // Start is called before the first frame update
    void Start()
    {
        connectedObject = new FruitsElectrics[2];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Connect(FruitsElectrics connected)
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

    public void Dissconnect(FruitsElectrics connected)
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

    public FruitsElectrics GetOther(FruitsElectrics connected)
    {
        return connectedObject[0] == connected ? connectedObject[1] : connectedObject[0];
    }
}
