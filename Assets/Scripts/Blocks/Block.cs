using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Block : MonoBehaviour
{
    public CodeController codeController;
    public GameObject editorBlock;

    public Block nextBlock;

    public virtual void Do()
    {
        if (nextBlock)
        {
            nextBlock.Do();
        }
    }

    public virtual void AddBlock(Block block)
    {
        nextBlock = block;
    }

    public virtual void Resolve(BlockData blockData)
    {
    }
 }
