using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class HeightController : MonoBehaviour {

    private bool isItemGrabbed;
    public GameObject movableObject;
    public GameObject dynamicalObject; 

    Vector3 offset;
    Vector3[] dynamicalOffset;

    private float minHeight;
    private float maxHeight;

    public GameObject[] dynamicalObjects;

    public static float counterHeight;

    // Use this for initialization
    void Start () {


        dynamicalOffset = new Vector3[dynamicalObjects.Length];
        for (int i = 0; i < dynamicalObjects.Length; i++)
        {
            dynamicalOffset[i] = dynamicalObjects[i].transform.position - movableObject.transform.position;

        }
        offset = movableObject.transform.position - this.transform.position;

        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        movableObject.transform.position = this.transform.position + offset;

        for (int i = 0; i < dynamicalObjects.Length; i++)
        {
            dynamicalObjects[i].transform.position = movableObject.transform.position + dynamicalOffset[i];

        }
        minHeight = 0.442f;
        maxHeight = 0.907f;

        this.gameObject.GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += IsItemGrabbed;
        this.gameObject.GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed += ItemNotGrabbed;
    }
	
	// Update is called once per frame
	void Update () {
       
        if (this.transform.position.y > maxHeight)
        {
            transform.position = new Vector3(this.transform.position.x, maxHeight, this.transform.position.z);
            counterHeight = this.transform.position.y;
        }
        if (this.transform.position.y <  minHeight)
        {
            transform.position = new Vector3(this.transform.position.x, minHeight, this.transform.position.z);
            counterHeight = this.transform.position.y;
        }

        if (isItemGrabbed)
        {
            movableObject.transform.position = this.transform.position + offset;
            dynamicalObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            counterHeight = this.transform.position.y;

          /*  for (int i = 0; i < dynamicalObjects.Length; i++)
            {
                dynamicalObjects[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
                dynamicalObjects[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
                dynamicalObjects[i].transform.position = new Vector3 (dynamicalObjects[i].transform.position.x, movableObject.transform.position.y + dynamicalOffset[i].y, dynamicalObjects[i].transform.position.z);

            }
        } else
        {
            for (int i = 0; i < dynamicalObjects.Length; i++)
            {
                dynamicalObjects[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            }*/

        }
   
    }

    private void IsItemGrabbed(object sender, InteractableObjectEventArgs e)
    {
        isItemGrabbed = true; 
    }


    private void ItemNotGrabbed(object sender, InteractableObjectEventArgs e)
    {
        isItemGrabbed = false;
    }

}
