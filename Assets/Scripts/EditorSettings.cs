using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorSettings : MonoBehaviour
{
    public bool enableSnap;
    public float snapMoveStep;
    public float snapRotateStepY;
    public float snapRotateStepX;
    public float snapRotateStepZ;
    public float snapScaleStep;

    public static EditorSettings instance;

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

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
