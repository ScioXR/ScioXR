using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine.Events;
using System;
using UnityEngine;

public class XRInputManager : MonoBehaviour
{
    public static bool EnableRight { get; set; } = false;
    [SerializeField] XRController controller;
    [SerializeField] XRBinding[] bindings;

    private void Update()
    {
        foreach (var binding in bindings)
        {
            binding.Update(controller.inputDevice);
        }
    }

    [Serializable]
    public class XRBinding
    {
        [SerializeField] XRButton button;
        [SerializeField] UnityEvent PressEvents;
        [SerializeField] UnityEvent IsNotPressEvents;
        bool isPressed;       
        bool clicked = false;

        public void Update(InputDevice device)
        {
            device.TryGetFeatureValue(XRStatics.GetFeature(button), out isPressed);
            if (isPressed)
            {
                if (!clicked)
                {
                    PressEvents.Invoke();
                    clicked = true;
                }
                else
                {
                    IsNotPressEvents.Invoke();                    
                }
            }
            else
            {               
                clicked = false;
            }
        }
    }

    public enum XRButton
    {
        Primary,
        Secondary
    }

    public static class XRStatics
    {
        public static InputFeatureUsage<bool> GetFeature(XRButton button)
        {
            switch (button)
            {
                case XRButton.Primary: return CommonUsages.primaryButton;
                case XRButton.Secondary: return CommonUsages.secondaryButton;
                default: Debug.LogError("button " + button + " not found "); return CommonUsages.primaryButton;
            }
        }
    }
}
