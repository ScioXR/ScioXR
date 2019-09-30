using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour
{
    public Transform lookRoot;
    public Transform lookPlayer;

    public bool invert;

    public bool can_Unlock = true;

    public float sensivity = 5f;

    public int smooth_Steps = 10;

    public float smooth_Weight = 0.4f;

    public float roll_Speed = 3f;

    public float roll_Angle = 10f;
    private Vector2 default_Look_Limits = new Vector2(-60f, 80f);
    private Vector2 look_Angles;
    private Vector2 current_Mouse_Look;
    private Vector2 smooth_Move;
    private float current_Roll_Angle;
    private int last_Look_Frame;



    // Start is called before the first frame update
    void Start()
    {
       // Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        LookAroundMouse();
    
}
    void LockAndUnLockMouse()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;

            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                //Cursor.lockState = CursorLockMode.None;
                //Cursor.visible = false;

            }
        }
    }
    void LookAroundMouse()
    {

        current_Mouse_Look = new Vector2(
            Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));

        look_Angles.x += current_Mouse_Look.x * sensivity * (invert ? 1f : -1f);
        look_Angles.y += current_Mouse_Look.y * sensivity;

        look_Angles.x = Mathf.Clamp(look_Angles.x, default_Look_Limits.x, default_Look_Limits.y);

        current_Roll_Angle = Mathf.Lerp(current_Roll_Angle, Input.GetAxisRaw("Mouse X") * roll_Angle, Time.deltaTime * roll_Speed);
        lookPlayer.localRotation = Quaternion.Euler(look_Angles.x, 0f, current_Roll_Angle);
        lookRoot.localRotation = Quaternion.Euler(0f, look_Angles.y, 0f);

    }
}