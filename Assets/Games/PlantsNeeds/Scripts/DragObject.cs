using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector3 mOffset;                                        //difference between the pos of the game object and the cursor in the 3D world.
    private float mZcoord;

    private void OnMouseDown()
    {
        Debug.Log("Chatch");
        mZcoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();    //Store offset = gameobject world pos - mouse worl pos.
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;                   //pixel coordinates (x,y)
        mousePoint.z = mZcoord;                                     //z coordinate of the gameobject on the screen.
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()                                      //When the object is draged with mouse.
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }
}
