using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecyclingGame : MonoBehaviour
{
    public GameObject[] showItems;

    public TextMeshPro pointsText;

    public GameObject trashSpawner;
    public GameObject trashPlace;
    public GameObject[] trashGroup;

    public int pipeSelected;
    public GameObject[] pipeButtons;
    public GameObject[] pipeOpenings;

    public int totalPoints;

    public PopupObject correctSign;
    public PopupObject wrongSign;
    public GameObject winObject;

    public TruckDeliveringGame deliveringGame;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var trash in trashGroup)
        {
            trash.SetActive(false);
        }
        foreach (var item in showItems)
        {
            item.SetActive(false);
        }

        //testing
        //StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTrash();
    }

    public void StartGame()
    {
        foreach (var item in showItems)
        {
            item.SetActive(true);
        }
        SelectPipe(pipeSelected);
    }

    public void SelectPipe(int pipeIndex)
    {
        pipeSelected = pipeIndex;
        for (int i = 0; i < pipeButtons.Length; i++)
        {
            pipeButtons[i].GetComponent<MeshRenderer>().material.color = (i == pipeSelected ? Color.green : Color.white);
        }

    }

    public float movingTime;
    private float timePassed;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool moving;
    private GameObject movingObject;

    public List<GameObject> trashSpawned = new List<GameObject>();

    public void ThrowTrash()
    {
        if (!moving)
        {
            movingObject = Instantiate(trashGroup[Random.Range(0, trashGroup.Length)]);
            movingObject.SetActive(true);
            moving = true;
            timePassed = 0;
            startPosition = trashSpawner.transform.position;
            endPosition = trashPlace.transform.position;
            trashSpawned.Add(movingObject);
        }
    }

    public void MoveTrash()
    {
        if (moving)
        {
            timePassed += Time.deltaTime;
            float moveStep = timePassed / movingTime;
            if (moveStep > 1)
            {
                moveStep = 1;
                moving = false;
            }
            movingObject.transform.position = Vector3.Lerp(startPosition, endPosition, moveStep);
        }
    }

    public void ConsumeTrash(GameObject trash, bool isGood)
    {
        if (!trashSpawned.Contains(trash))
        {
            return;
        }
        trashSpawned.Remove(trash);

        Destroy(trash);
        if (isGood)
        {
            totalPoints++;

            pointsText.text = totalPoints + "/10";
            correctSign.Show(2);
            if (totalPoints >= 10)
            {
                Debug.Log("Recycling WIN");
                Invoke("EndGame", 2);
            }
        } else
        {
            wrongSign.Show(2);
        }
        
    }

    public void EndGame()
    {
        pointsText.gameObject.SetActive(false);
        winObject.SetActive(true);
        deliveringGame.StartGame();
    }

    public void SendTrash(GameObject trash)
    {
        if (trash.tag == "plastic" || trash.tag == "metal" || trash.tag == "paper")
        {
            trash.transform.position = pipeOpenings[pipeSelected].transform.position;
        }
    }
}
