using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveController : MonoBehaviour
{
    public float walkingSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        forward.y = 0;
        Vector3 right = transform.TransformDirection(Vector3.right);
        if (Keyboard.current.wKey.isPressed)
        {
            transform.position += forward * walkingSpeed * Time.deltaTime;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            transform.position += forward * -walkingSpeed * Time.deltaTime;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            transform.position += right * -walkingSpeed * Time.deltaTime;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            transform.position += right * walkingSpeed * Time.deltaTime;
        }
    }
}
