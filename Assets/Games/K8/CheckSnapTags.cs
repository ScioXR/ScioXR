using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckSnapTags : MonoBehaviour
{
    public SnapPlace[] snapPlaces;

    public int tagsForWin;

    public GameObject correctSign;
    public GameObject wrongSign;

    public UnityEvent onCorrect;
    public UnityEvent onWrong;

    public bool shouldPoll;

    private void Update()
    {
        if (shouldPoll)
        {
            bool win = CheckTags();
            if (correctSign)
            {
                correctSign.SetActive(win);
            }
            if (wrongSign)
            {
                wrongSign.SetActive(!win);
            }
        }
    }

    // Update is called once per frame
    bool CheckTags()
    {
        int goodTags = 0;
        for (int i = 0; i < snapPlaces.Length; i++)
        {
            if (snapPlaces[i].snappedObject && snapPlaces[i].snappedObject.tag == snapPlaces[i].gameObject.tag)
            {
                goodTags++;
            }
        }

        bool win = (goodTags >= tagsForWin);
        return win;
    }

    public void CheckWin()
    {
        bool win = CheckTags();
        if (win)
        {
            onCorrect.Invoke();
        } else
        {
            onWrong.Invoke();
        }
    }
}
