using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VRTK;

public class ButtonListener : MonoBehaviour
{
    public UnityEvent buttonTwoPressEvents;
    public UnityEvent buttonTwoReleaseEvents;

    public UnityEvent startMenuPressEvents;
    public UnityEvent startMenuReleaseEvents;


    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<VRTK_Pointer>().enabled = true;
        GetComponent<VRTK_ControllerEvents>().ButtonTwoPressed += new ControllerInteractionEventHandler(DoButtonTwoPress);
        GetComponent<VRTK_ControllerEvents>().ButtonTwoReleased += new ControllerInteractionEventHandler(DoButtonTwoReleased);

        GetComponent<VRTK_ControllerEvents>().StartMenuPressed += new ControllerInteractionEventHandler(DoStartMenuPress);
        GetComponent<VRTK_ControllerEvents>().StartMenuReleased += new ControllerInteractionEventHandler(DoStartMenuReleased);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DoButtonTwoPress(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("buttton two");
        buttonTwoPressEvents.Invoke();
    }

    private void DoButtonTwoReleased(object sender, ControllerInteractionEventArgs e)
    {
        buttonTwoReleaseEvents.Invoke();
    }

    private void DoStartMenuPress(object sender, ControllerInteractionEventArgs e)
    {
        startMenuPressEvents.Invoke();
    }

    private void DoStartMenuReleased(object sender, ControllerInteractionEventArgs e)
    {
        startMenuReleaseEvents.Invoke();
    }
}
