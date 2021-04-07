using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhysicsPanel : XRPanel
{
    public InputField mass;

    public override void Show()
    {
        base.Show();
        mass.text = "" + EditorManager.instance.selectedObject.GetComponent<Saveable>().data.physics.mass;
    }

    public void UpdateMass()
    {
        float fMass = float.Parse(mass.text);
        EditorManager.instance.selectedObject.GetComponent<Saveable>().data.physics.mass = fMass;
    }
}
