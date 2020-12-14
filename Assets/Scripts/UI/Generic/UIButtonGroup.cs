using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonGroup : MonoBehaviour
{
    public UIButtonToggle[] buttons;

    public int selected;

    private void Start()
    {
        Select(buttons[selected]);
    }

    public void Select(UIButtonToggle button)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].Set(button == buttons[i]);
            if (button == buttons[i])
            {
                selected = i;
            }
        }
    }
}
