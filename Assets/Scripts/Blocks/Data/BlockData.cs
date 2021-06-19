using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class BlockData
{
    public static int lastId = 1;
    public static List<BlockData> allBlocks = new List<BlockData>();

    public int id;
    public bool rootObject;
    public string blockType;
    public Vector2 editorPosition;
    public int paramInt;
    public string paramString;
    public int objectReference;

    [NonSerialized]
    public BlockData[] childBlocks;
    public int[] childBlocksIds;

    [NonSerialized]
    public BlockData[] attachedBlocks;
    public int[] attachedBlocksIds;

    [NonSerialized]
    public BlockData[] blocks;
    public int[] blocksIds;

    public BlockData()
    {
        id = lastId++;
        allBlocks.Add(this);
    }

    ~BlockData()
    {
        allBlocks.Remove(this);
    }

    public void ImportBlockReferences(BlockData[] allBlocks)
    {
        if (blocksIds != null && blocksIds.Length > 0)
        {
            blocks = new BlockData[blocksIds.Length];
            for (int i = 0; i < blocksIds.Length; i++)
            {
                blocks[i] = allBlocks.First(item => item.id == blocksIds[i]);
            }
        }

        if (childBlocksIds != null && childBlocksIds.Length > 0)
        {
            childBlocks = new BlockData[childBlocksIds.Length];
            for (int i = 0; i < childBlocksIds.Length; i++)
            {
                childBlocks[i] = allBlocks.First(item => item.id == childBlocksIds[i]);
            }
        }

        if (attachedBlocksIds != null && attachedBlocksIds.Length > 0)
        {
            attachedBlocks = new BlockData[attachedBlocksIds.Length];
            for (int i = 0; i < attachedBlocksIds.Length; i++)
            {
                attachedBlocks[i] = allBlocks.First(item => item.id == attachedBlocksIds[i]);
            }
        }
    }

    public void ExportBlockReferences()
    {
        if (blocks != null && blocks.Length > 0)
        {
            blocksIds = new int[blocks.Length];
            for (int i = 0; i < blocksIds.Length; i++)
            {
                blocksIds[i] = blocks[i].id;
            }
        }

        if (childBlocks != null && childBlocks.Length > 0)
        {
            childBlocksIds = new int[childBlocks.Length];
            for (int i = 0; i < childBlocksIds.Length; i++)
            {
                childBlocksIds[i] = childBlocks[i].id;
            }
        }

        if (attachedBlocks != null && attachedBlocks.Length > 0)
        {
            attachedBlocksIds = new int[attachedBlocks.Length];
            for (int i = 0; i < attachedBlocksIds.Length; i++)
            {
                attachedBlocksIds[i] = attachedBlocks[i].id;
            }
        }
    }
}
