using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentMenu : MonoBehaviour
{
    public Renderer[] rend;
    public Material[] materials;

    public void ChangeEnvironmentColor (int index)
    {
        for (int i = 0; i < rend.Length; i++)
        {
            rend[i].GetComponent<MeshRenderer>().material = materials[index];
        }
    }

}
