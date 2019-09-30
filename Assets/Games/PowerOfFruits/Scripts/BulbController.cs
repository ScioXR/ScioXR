using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulbController : FruitsElectrics
{
    public static BulbController instance;
    public float totalElectrics = 0;
    public Light bulbLight;

    public TextMeshPro text;

    // Start is called before the first frame update
    void Awake()
    {
        if (!instance)
        {
            instance = this;

        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        connectedObject = new GameObject[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (totalElectrics >= 100)
        {
            bulbLight.enabled = true;
            bulbLight.intensity = 2f;
        } else
        {
            bulbLight.enabled = false;
            //bulbLight.intensity = 2f;
        }
    }

    public override float GetElectricity(ControllerWire wire)
    {
        return 0;
    }


    public void CalculateElectrics()
    {
        totalElectrics = 0;
        if (IsGoodConnection())
        {
           // Debug.Log("Connected: " + connectedObject[0].name);
            totalElectrics = connectedObject[0].transform.parent.GetComponent<ControllerWire>().GetOther(this).GetElectricity(connectedObject[0].transform.parent.GetComponent<ControllerWire>());
        }
       // Debug.Log("totalElectrics " + totalElectrics);
        text.text = "Electrics: " + totalElectrics;
    }
}
