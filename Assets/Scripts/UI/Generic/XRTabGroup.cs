using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRTabGroup : XRPanel
{
    public XRPanel[] tabs;
    private int selectedTab;

    public override void Show()
    {
        base.Show();
        SelectTab(selectedTab);
    }
  

    public void SelectTab(int index)
    {
        //Debug.Log("SelectTab: " + index);
        selectedTab = index;

        for (int i = 0; i < tabs.Length; i++)
        {
            if (index != i)
            {
                tabs[i].Hide();
            }
        } 

        tabs[index].Show();
    }
}
