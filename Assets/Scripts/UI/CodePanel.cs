using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodePanel : XRPanel
{
    public CodeBoard codeBoard;

    public override void Show()
    {
        base.Show();
        //Debug.Log("CodePanel show: " + EditorManager.instance.selectedObject.name);
        LoadState();
    }

    public override void Hide()
    {
        base.Hide();
        //Debug.Log("CodePanel hide: " + EditorManager.instance.selectedObject.name);
        SaveState();
    }

    public void LoadState()
    {
        codeBoard.LoadCode(EditorManager.instance.selectedObject.GetComponent<Saveable>().data.code);
    }

    public void SaveState()
    {
        if (codeBoard.shouldSave)
        {
            EditorManager.instance.selectedObject.GetComponent<Saveable>().data.code = codeBoard.SaveCode();
            EditorManager.instance.globalData.variables = codeBoard.blockBoard.GetVariables().ToArray();
            EditorManager.instance.globalData.messages = codeBoard.blockBoard.GetMessages().ToArray();
            codeBoard.shouldSave = false;
        }
    }
}
