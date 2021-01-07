using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FoodChainGame : MonoBehaviour
{
    public SnapPlace[] snapPlaces;
    public GameObject[] snapObjects;

    public RecyclingGame recyclingGame;

    public GameObject gameText;
    public GameObject correctSign;
    public GameObject wrongSign;

    bool running;

    // Start is called before the first frame update
    void Start()
    {
        running = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (running) {
            CheckWin();
        }
    }

    private void CheckWin()
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
        gameText.SetActive(filled != snapPlaces.Length);
        correctSign.SetActive(filled == snapPlaces.Length && win);
        wrongSign.SetActive(filled == snapPlaces.Length && !win);
        if (win)
        {
            running = false;
            foreach (var snapObject in snapObjects)
            {
                Destroy(snapObject.GetComponent<WebXRGrabInteractable>());
                Destroy(snapObject.GetComponent<Snappable>());
            }
            recyclingGame.StartGame();
        }
    }
}
