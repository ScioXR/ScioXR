using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CheckSnappableOrder : MonoBehaviour
{
    public SnapPlace[] snapPlaces;
    public GameObject[] snapObjects;

    public GameObject correctSign;
    public GameObject wrongSign;

    // Update is called once per frame
    void Update()
    {
        bool win = true;
        for (int i = 0; i < snapPlaces.Length; i++)
        {
            if (snapPlaces[i].snappedObject != snapObjects[i])
            {
                win = false;
                break;
            }
        }
        int filled = snapPlaces.Count(place => place.snappedObject != null);

        if (correctSign)
        {
            correctSign.SetActive(filled == snapPlaces.Length && win);
        }
        if (wrongSign)
        {
            wrongSign.SetActive(filled == snapPlaces.Length && !win);
        }
    }
}
