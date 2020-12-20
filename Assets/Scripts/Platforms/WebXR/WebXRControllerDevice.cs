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

    [InputControl(name = "trigger", layout = "Axis", usages = new[] { "PrimaryTrigger" }, bit = 0)]
    public float trigger;

    [InputControl(name = "primaryButton", layout = "Button", usages = new[] { "PrimaryAction", "Submit" }, bit = 0)]
    [InputControl(name = "secondaryButton", layout = "Button", usages = new[] { "Menu" }, bit = 1)]
    public int buttons;
}

[InputControlLayout(displayName = "WebXR Controller", stateType = typeof(WebXRControllerDeviceState), commonUsages = new[] { "LeftHand", "RightHand" })]
[InitializeOnLoad]
public class WebXRControllerDevice : InputDevice
{

    public static WebXRControllerDevice leftHand => InputSystem.GetDevice<WebXRControllerDevice>(CommonUsages.LeftHand);
    public static WebXRControllerDevice rightHand => InputSystem.GetDevice<WebXRControllerDevice>(CommonUsages.RightHand);

    [InputControl]
    public AxisControl trigger { get; private set; }

    [InputControl]
    public ButtonControl primaryButton { get; private set; }

    [InputControl]
    public ButtonControl secondaryButton { get; private set; }

    protected override void FinishSetup()
    {
        base.FinishSetup();

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