using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashButton : MonoBehaviour, IWebXRInteractable
{
    public RecyclingGame recyclingGame;

    void IWebXRInteractable.OnGrab(WebXRInteractor interactor)
    {
    }

    void IWebXRInteractable.OnSecondaryGrab(WebXRInteractor interactor)
    {

    }

    void IWebXRInteractable.OnSecondaryUngrab(WebXRInteractor interactor)
    {

    }

    void IWebXRInteractable.OnTouch(WebXRInteractor interactor)
    {
        recyclingGame.ThrowTrash();
    }

    void IWebXRInteractable.OnUngrab(WebXRInteractor interactor)
    {

    }

    void IWebXRInteractable.OnUntouch(WebXRInteractor interactor)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
