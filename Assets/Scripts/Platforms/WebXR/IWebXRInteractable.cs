using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWebXRInteractable
{
    void OnGrab(WebXRInteractor interactor);
    void OnUngrab(WebXRInteractor interactor);
    void OnSecondaryGrab(WebXRInteractor interactor);
    void OnSecondaryUngrab(WebXRInteractor interactor);
}

