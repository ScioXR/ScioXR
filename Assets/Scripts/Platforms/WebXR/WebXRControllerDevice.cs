using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.XR;
using UnityEngine.Scripting;

public struct WebXRControllerDeviceState : IInputStateTypeInfo
{
    // FourCC type codes are used to identify the memory layouts of state blocks.
    public FourCC format => new FourCC('W', 'B', 'X', 'R');

    [InputControl(name = "thumbstick", layout = "Stick", usages = new[] { "Primary2DMotion" })]
    public Vector2 thumbstick;

    [InputControl(name = "trigger", layout = "Axis", usages = new[] { "PrimaryTrigger" }, bit = 0)]
    public float trigger;

    [InputControl(name = "grip", layout = "Axis", bit = 0)]
    public float grip;

    [InputControl(name = "triggerPressed", layout = "Button", bit = 0)]
    [InputControl(name = "gripPressed", layout = "Button", bit = 1)]
    [InputControl(name = "primaryButton", layout = "Button", usages = new[] { "PrimaryAction", "Submit" }, bit = 2)]
    [InputControl(name = "secondaryButton", layout = "Button", usages = new[] { "Menu" }, bit = 3)]
    public int buttons;

    public void CopyFrom(WebXRControllerDeviceState copyState)
    {
        thumbstick = copyState.thumbstick;
        trigger = copyState.trigger;
        grip = copyState.grip;
        buttons = copyState.buttons;
        thumbstick = copyState.thumbstick;
    }

    public static bool operator ==(WebXRControllerDeviceState c1, WebXRControllerDeviceState c2)
    {
        return c1.Equals(c2);
    }

    public static bool operator !=(WebXRControllerDeviceState c1, WebXRControllerDeviceState c2)
    {
        return !c1.Equals(c2);
    }

    public override bool Equals(object obj)
    {
        WebXRControllerDeviceState otherState = (WebXRControllerDeviceState)obj;
        return thumbstick == otherState.thumbstick && trigger == otherState.trigger && grip == otherState.grip && buttons == otherState.buttons && thumbstick == otherState.thumbstick;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

[InputControlLayout(displayName = "WebXR Controller", stateType = typeof(WebXRControllerDeviceState), commonUsages = new[] { "LeftHand", "RightHand" })]
[InitializeOnLoad]
public class WebXRControllerDevice : InputDevice
{

    public static WebXRControllerDevice leftHand => InputSystem.GetDevice<WebXRControllerDevice>(CommonUsages.LeftHand);
    public static WebXRControllerDevice rightHand => InputSystem.GetDevice<WebXRControllerDevice>(CommonUsages.RightHand);

    [InputControl(aliases = new[] { "Primary2DAxis", "Joystick" })]
    public Vector2Control thumbstick { get; private set; }

    [InputControl]
    public AxisControl trigger { get; private set; }

    [InputControl(aliases = new[] { "indexButton", "indexTouched" })]
    public ButtonControl triggerPressed { get; private set; }

    [InputControl]
    public AxisControl grip { get; private set; }

    [InputControl(aliases = new[] { "GripButton" })]
    public ButtonControl gripPressed { get; private set; }

    [InputControl(aliases = new[] { "A", "X", "Alternate" })]
    public ButtonControl primaryButton { get; private set; }

    [InputControl(aliases = new[] { "B", "Y", "Primary" })]
    public ButtonControl secondaryButton { get; private set; }

    protected override void FinishSetup()
    {
        base.FinishSetup();

        thumbstick = GetChildControl<Vector2Control>("thumbstick");
        trigger = GetChildControl<AxisControl>("trigger");
        triggerPressed = GetChildControl<ButtonControl>("triggerPressed");
        grip = GetChildControl<AxisControl>("grip");
        gripPressed = GetChildControl<ButtonControl>("gripPressed");
        primaryButton = GetChildControl<ButtonControl>("primaryButton");
        secondaryButton = GetChildControl<ButtonControl>("secondaryButton");
    }

    static WebXRControllerDevice()
    {
        InputSystem.RegisterLayout<WebXRControllerDevice>();
    }

    [RuntimeInitializeOnLoadMethod]
    static void Init() { }
}