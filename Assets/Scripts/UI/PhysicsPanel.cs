using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhysicsPanel : XRPanel
{
    public InputField mass;
    public InputField drag;
    public InputField angularDrag;

    public GameObject settings;
    public GameObject notAvailable;

    public override void Show()
    {
        base.Show();
        mass.text = "" + EditorManager.instance.selectedObject.GetComponent<Saveable>().data.physics.mass;
        drag.text = "" + EditorManager.instance.selectedObject.GetComponent<Saveable>().data.physics.drag;
        angularDrag.text = "" + EditorManager.instance.selectedObject.GetComponent<Saveable>().data.physics.angularDrag;

        bool physicsAvailable = EditorManager.instance.selectedObject.GetComponent<Saveable>().data.isInteractable;
        settings.SetActive(physicsAvailable);
        notAvailable.SetActive(!physicsAvailable);
    }

    public void UpdateData()
    {
        float fMass = float.Parse(mass.text);
        EditorManager.instance.selectedObject.GetComponent<Saveable>().data.physics.mass = fMass;
        EditorManager.instance.selectedObject.GetComponent<Saveable>().data.physics.drag = float.Parse(drag.text);
        EditorManager.instance.selectedObject.GetComponent<Saveable>().data.physics.angularDrag = float.Parse(angularDrag.text);
    }
}
