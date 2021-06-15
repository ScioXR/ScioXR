using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupInstance : MonoBehaviour
{
    public SetupInstance instance;

    private void Awake()
    {
        if (!instance)
        {          
            DontDestroyOnLoad(this.gameObject);
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
