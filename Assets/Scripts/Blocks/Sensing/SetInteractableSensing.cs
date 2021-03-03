using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInteractableSensing : Block
{
    private bool setInteractable;
    public override void Do()
    {
        base.Do();
        PlatformLoader.instance.platform.SetInteractable(gameObject, setInteractable);
    }

    public override void Resolve(BlockData blockData)
    {
        base.Resolve(blockData);
        setInteractable = blockData.paramInt > 0;
    }
}
