using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeButton : MonoBehaviour, IWebXRInteractable
{
    public int buttonIndex;
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
        recyclingGame.SelectPipe(buttonIndex);
    }

    void IWebXRInteractable.OnUngrab(WebXRInteractor interactor)
    {

    }

    void IWebXRInteractable.OnUntouch(WebXRInteractor interactor)
    {

    }
}
