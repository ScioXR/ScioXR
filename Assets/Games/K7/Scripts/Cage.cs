using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    int unlockedLights;
    public GameObject[] lights;
    public GameObject lockObject;

    public Material green;

    public void UnlockLight()
    {
        lights[unlockedLights].GetComponent<MeshRenderer>().material = green;
        unlockedLights++;
        if (unlockedLights >= lights.Length)
        {
            lockObject.SetActive(false);
        }
    }
}
