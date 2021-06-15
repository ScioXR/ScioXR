using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeController : MonoBehaviour
{
    public int id;

    public List<Block> blocks = new List<Block>();

    public void LoadCode(CodeData codeData)
    {
        foreach (var blockData in codeData.blocks)
        {
            Block block = Resolve(blockData);
            if (block is Event)
            {
                blocks.Add(block);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (AppManager.instance.loaded)
        {
            foreach (var block in blocks)
            {
                if (block is Event)
                {
                    (block as Event).Poll();
                }
            }
        }
    }

    public Block Resolve(BlockData blockData)
    {
        Block result = null;
        if (blockData.blockType == "StartEvent")
        {
            result = gameObject.AddComponent<StartEvent>();
        }
        else if (blockData.blockType == "IfControl")
        {
            result = gameObject.AddComponent<IfControl>();
        }
        else if (blockData.blockType == "WaitControl")
        {
            result = gameObject.AddComponent<WaitControl>();
        }
        else if (blockData.blockType == "MoveMotion")
        {
            result = gameObject.AddComponent<MoveMotion>();
        }
        else if (blockData.blockType == "PointsTowardMotion")
        {
            result = gameObject.AddComponent<PointsTowardMotion>();
        }
        else if (blockData.blockType == "Variable")
        {
            result = VariablesManager.instance.GetVariable(blockData.paramString);
        }
        else if (blockData.blockType == "SetVariable")
        {
            result = gameObject.AddComponent<SetVariable>();
        }
        else if (blockData.blockType == "IntVariable")
        {
            result = gameObject.AddComponent<IntVariable>();
        }
        else if (blockData.blockType == "StringVariable")
        {
            result = gameObject.AddComponent<StringVariable>();
        }
        else if (blockData.blockType == "AddVariable")
        {
            result = gameObject.AddComponent<AddVariable>();
        }
        else if (blockData.blockType == "SubtractVariable")
        {
            result = gameObject.AddComponent<SubtractVariable>();
        }
        else if (blockData.blockType == "MultiplyVariable")
        {
            result = gameObject.AddComponent<MultiplyVariable>();
        }
        else if (blockData.blockType == "DivideVariable")
        {
            result = gameObject.AddComponent<DivideVariable>();
        }
        else if (blockData.blockType == "EqualsCondition")
        {
            result = gameObject.AddComponent<EqualsCondition>();
        }
        else if (blockData.blockType == "LessCondition")
        {
            result = gameObject.AddComponent<LessCondition>();
        }
        else if (blockData.blockType == "GreaterCondition")
        {
            result = gameObject.AddComponent<GreaterCondition>();
        }
        else if (blockData.blockType == "ShowLook")
        {
            result = gameObject.AddComponent<ShowLook>();
        }
        else if (blockData.blockType == "HideLook")
        {
            result = gameObject.AddComponent<HideLook>();
        }
        else if (blockData.blockType == "SetTextLook")
        {
            result = gameObject.AddComponent<SetTextLook>();
        }
        else if (blockData.blockType == "SetColorLook")
        {
            result = gameObject.AddComponent<SetColorLook>();
        }
        else if (blockData.blockType == "DestroyControl")
        {
            result = gameObject.AddComponent<DestroyControl>();
        }
        else if (blockData.blockType == "ReceiveEvent")
        {
            result = gameObject.AddComponent<ReceiveEvent>();
        }
        else if (blockData.blockType == "BroadcastEvent")
        {
            result = gameObject.AddComponent<BroadcastEvent>();
        }
        else if (blockData.blockType == "InteractEvent")
        {
            result = gameObject.AddComponent<InteractEvent>();
        }
        else if (blockData.blockType == "SetInteractableSensing")
        {
            //result = gameObject.AddComponent<InteractEvent>();
        }
        result.codeController = this;
        result.Resolve(blockData);

        //resolve childs
        Block lastBlock = result;
        foreach (var childBlockData in blockData.blocks)
        {
            Block childBlock = Resolve(childBlockData);
            lastBlock.AddBlock(childBlock);
            lastBlock = childBlock;
        }

        return result;
    }

    public void Interact()
    {
        InteractEvent[] interactEvents = GetComponents<InteractEvent>();
        foreach (var interactEvent in interactEvents)
        {
            interactEvent.Trigger();
        }
    }
}
