using HSVPicker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPanel : XRPanel
{
    public InputField text;
    public InputField textSize;

    public ColorPicker colorPicker;

    public override void Show()
    {
        base.Show();
        text.text = "" + EditorManager.instance.selectedObject.GetComponent<Saveable>().data.text.text;
        textSize.text = "" + EditorManager.instance.selectedObject.GetComponent<Saveable>().data.text.size;

        Color color = Color.white;
        ColorUtility.TryParseHtmlString("#" + EditorManager.instance.selectedObject.GetComponent<Saveable>().data.text.color, out color);
        colorPicker.CurrentColor = color;
    }

    public void UpdateData()
    {
        EditorManager.instance.selectedObject.GetComponent<Saveable>().data.text.text = text.text;
        EditorManager.instance.selectedObject.GetComponent<Saveable>().data.text.size = int.Parse(textSize.text);
        EditorManager.instance.selectedObject.GetComponent<Saveable>().UpdateText();
    }

    public void SelectTextColor(Color color)
    {
        EditorManager.instance.selectedObject.GetComponent<Saveable>().data.text.color = ColorUtility.ToHtmlStringRGBA(color);
        EditorManager.instance.selectedObject.GetComponent<Saveable>().UpdateText();
    }
}
