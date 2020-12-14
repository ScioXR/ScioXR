using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BlockData
{
    public string blockType;
    public Vector2 editorPosition;
    public int paramInt;
    public string paramString;
    public int objectReference;

    public BlockData[] blocks;
}
